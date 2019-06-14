"use strict";

if (!store.state['hospitals']) {
    const hospitalsModule = {
        state: {
            hospitals: []
        },
        mutations: {
            addHospital(state, hospital) {
                let index = state.hospitals.findIndex(h => h.id == hospital.id);
                if (index == -1) {
                    state.hospitals.push(hospital);
                } else {
                    Vue.set(state.hospitals, index, hospital);
                }
            },
            setHospitals(state, hospitals) {
                state.hospitals = hospitals;
            }
        }
    };
    store.registerModule('hospitals', hospitalsModule);
    DATA_CLIENT.getHospitals().then(result => {
        store.commit('setHospitals', result);
    });
}

Vue.component('Hospitals', {
    template: '#hospitals-template',
    data: function () {
        return {
            selectedHospital: {},
            editingHospital: {},
            editing: false,
            regions: [],
            regionsLoading: false,
            districts: [],
            districtsLoading: false,
            cities: [],
            citiesLoading: false,
            cityName:'',
            streets: [],
            streetsLoading: false,
            saveConfirmation: false,
            quitConfirmation: false,
            addressConfirmation: false,
            addressChangeConfirmation:false
        }
    },
    store,
    methods: {
        newHospital: function () {
            return {
                name: '',
                shortName: '',
                address: {
                    countryId: 71,
                    regionId: '',
                    districtId: '',
                    cityId: '',
                    streetId: '',
                    addressTypeId:1
                }
            }
        },
        hospitalClicked: function (hospital) {
            if (this.enabled) {
                this.selectedHospital = hospital;
            }
        },
        createNewHospital: function () {
            this.selectedHospital = this.newHospital();
            this.editingHospital = this.newHospital();
            this.cityName = '';
            this.editing = true;
            this.loadRegions();
            this.loadCities('country');
        },
        editHospital: function () {
            if (this.selectedHospital) {
                this.editingHospital = $.extend(true, {}, this.selectedHospital)
                this.cityName = this.editingHospital.address.city.name;
                this.editing = true;
                if (this.editingHospital.address.cityId) {
                    this.loadStreets();
                }
                if (this.editingHospital.address.regionId) {
                    this.loadDistricts();
                }
                if (this.editingHospital.address.countryId) {
                    this.loadRegions();
                }
                if (this.editingHospital.address.districtId) {
                    this.loadCities('district');
                } else if (this.editingHospital.address.regionId) {
                    this.loadCities('region');
                } else if (this.editingHospital.address.countryId) {
                    this.loadCities('country');
                }
            }
        },
        saveClicked: function () {
            $.validator.unobtrusive.parse($(this.$el));
            if ($(this.$el).valid()) {
                this.saveConfirmation = true;
            }
        },
        saveHospital: async function () {
            this.saveConfirmation = false;
            if (this.editingHospital.id) {
                if (this.addressChanged) {
                    this.addressChangeConfirmation = true;
                } else {
                    this.saveAndExit();
                }
            } else {
                let address = await DATA_CLIENT.checkAddressExists(this.editingHospital.address)
                if (address) {
                    this.editingHospital.address = address;
                    this.addressConfirmation = true;
                } else {
                    this.saveAndExit();
                }
            } 
        },
        changeOldAddress: function () {
            this.saveAndExit();
        },
        saveNewAddress: function () {
            this.editingHospital.address.id = 0;
            this.saveAndExit();
        },
        saveAndExit: async function () {
            this.editing = false;
            if (this.editingHospital.id) {
                this.editHospital = await DATA_CLIENT.editHospital(this.editingHospital);
            } else {
                this.editingHospital = await DATA_CLIENT.saveHospital(this.editingHospital);
            }
            this.$store.commit('addHospital', this.editingHospital);
        },
        cancelEdit: function () {
            this.editing = false;
            this.quitConfirmation = false;
            this.regions.splice(0);
            this.districts.splice(0);
            this.cities.splice(0);
            this.streets.splice(0);
        },
        addEmailToHospital: async function (email) {
            if (this.selectedHospital.id) {
                email.hospitalId = this.selectedHospital.id;
                return await DATA_CLIENT.addEmailToHospital(email);
            }
        },
        deleteEmailFromHospital: async function (email) {
            if (this.selectedHospital.id) {
                email.hospitalId = this.selectedHospital.id;
                return await DATA_CLIENT.deleteEmailFromHospital(email);
            }
        },
        addPhoneToHospital: async function (phone) {
            if (this.selectedHospital.id) {
                phone.hospitalId = this.selectedHospital.id;
                return await DATA_CLIENT.addPhoneToHospital(phone);
            }
        },
        deletePhoneFromHospital: async function (phone) {
            if (this.selectedHospital.id) {
                phone.hospitalId = this.selectedHospital.id;
                return await DATA_CLIENT.deletePhoneFromHospital(phone);
            }
        },
        addressChanged: function () {
            return this.selectedHospital.address.countryId != this.editingHospital.address.countryId
                || this.selectedHospital.address.regionId != this.editingHospital.address.regionId
                || this.selectedHospital.address.districtId != this.editingHospital.address.districtId
                || this.selectedHospital.address.cityId != this.editingHospital.address.cityId
                || this.selectedHospital.address.streetId != this.editingHospital.address.streetId
                || this.selectedHospital.address.building != this.editingHospital.address.building
                || this.selectedHospital.address.zip != this.editingHospital.address.zip;
        },
        selectedCountryChanged: function () {
            this.regions.splice(0);
            this.editingHospital.address.regionId = '';
            this.districts.splice(0);
            this.editingHospital.address.districtId = '';
            this.cities.splice(0);
            this.editingHospital.address.cityId = '';
            this.streets.splice(0);
            this.editingHospital.address.streetId = '';
            if (this.editingHospital.address.countryId) {
                this.loadRegions();
                this.loadCities('country');
            }

        },
        selectedRegionChanged: function () {
            this.districts.splice(0);
            this.editingHospital.address.districtId = '';
            this.cities.splice(0);
            this.editingHospital.address.cityId = '';
            this.streets.splice(0);
            this.editingHospital.address.streetId = '';
            this.loadDistricts();
            this.loadCities('region');
        },
        selectedDistrictChanged: function () {
            this.cities.splice(0);
            this.editingHospital.address.cityId = '';
            this.streets.splice(0);
            this.editingHospital.address.streetId = '';
            this.loadCities('district');
        },
        selectedCityChanged: function () {
            if (this.editingHospital.address.cityId == '$reset') {
                this.cityName = '';
                this.editingHospital.address.cityId = '';
            } else {
                this.streets.splice(0);
                this.editingHospital.address.streetId = '';
                this.loadStreets();
            }
            this.editingHospital.address.city = this.cities.find(c => c.id == this.editingHospital.address.cityId);
            if (this.editingHospital.address.city.regionId != this.editingHospital.address.regionId) {
                this.editingHospital.address.regionId = this.editingHospital.address.city.regionId;
                this.loadDistricts();
            }
            if (this.editingHospital.address.city.districtId != this.editingHospital.address.districtId) {
                this.editingHospital.address.districtId = this.editingHospital.address.city.districtId;
            }
        },
        selectedStreetChanged: function () {
            this.editingHospital.address.street = this.streets.find(s => s.id == this.editingHospital.address.streetId);
        },
        loadRegions: async function () {
            if (this.editingHospital.address.countryId) {
                this.regionsLoading = true;
                this.regions.push(...await DATA_CLIENT.getRegionsByCountry(this.editingHospital.address.countryId));
                this.regionsLoading = false;
            }
        },
        loadDistricts: async function () {
            if (this.editingHospital.address.regionId) {
                this.districtsLoading = true;
                this.districts.push(...await DATA_CLIENT.getDistrictsByRegion(this.editingHospital.address.regionId));
                this.districtsLoading = false;
            }
        },
        loadCities: async function (source) {
            this.citiesLoading = true;
            switch (source) {
                case 'country':
                    if (this.editingHospital.address.countryId) {
                        this.cities.push(...await DATA_CLIENT.getCitiesByCountry(this.editingHospital.address.countryId));
                    }
                    break;
                case 'region':
                    if (this.editingHospital.address.regionId) {
                        this.cities.push(...await DATA_CLIENT.getCitiesByRegion(this.editingHospital.address.regionId));
                    } else {
                        this.loadCities('country');
                        return;
                    }
                    break;
                case 'district':
                    if (this.editingHospital.address.districtId) {
                        this.cities.push(...await DATA_CLIENT.getCitiesByDistrict(this.editingHospital.address.districtId));
                    } else {
                        this.loadCities('region');
                        return;
                    }
                    break;
            }
            this.citiesLoading = false;
        },
        loadStreets: async function () {
            if (this.editingHospital.address.cityId) {
                this.streetsLoading = true;
                this.streets.push(...await DATA_CLIENT.getStreetsByCity(this.editingHospital.address.cityId));
                this.streetsLoading = false;
            }
        }
    },
    computed: {
        enabled: {
            get: function () {
                return store.state.enabled;
            },
            set: function (show) {
                store.commit('enable', show);
            }
        },
        filteredCities: function () {
            return this.cities.filter(c => c.name.toLowerCase().startsWith(this.cityName.toLowerCase()));
        },
        hospitals: {
            get: function () {
                return this.$store.state.hospitals.hospitals;
            },
            set: function (hospitals) {
                this.$store.commit('setHospitals', hospitals);
            }
        }
    },
    watch: {
        saveConfirmation: function (val) {
            if (val) {
                this.enabled = false;
            } else {
                this.enabled = true;
            }
        },
        quitConfirmation: function (val) {
            if (val) {
                this.enabled = false;
            } else {
                this.enabled = true;
            }
        },
        addressConfirmation: function (val) {
            if (val) {
                this.enabled = false;
            } else {
                this.enabled = true;
            }
        },
        addressChangeConfirmation: function (val) {
            if (val) {
                this.enabled = false;
            } else {
                this.enabled = true;
            }
        }
    },
    provide: function () {
        return {
            addEmail: this.addEmailToHospital,
            deleteEmail: this.deleteEmailFromHospital,
            addPhone: this.addPhoneToHospital,
            deletePhone: this.deletePhoneFromHospital
        }
    },
    created: async function () {
        //this.hospitals = store.state.hospitals.hospitals;
    }
});