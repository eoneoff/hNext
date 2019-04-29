"use strict";

Vue.component('PersonEditor', {
    template: "#person-editor-template",
    data: function () {
        return {
            person: $.extend(true, this.newPerson(), this.initialPerson),
            placeOfBirthName: '',
            cityName: '',
            regions: [],
            districts: [],
            cities: [],
            streets: [],
            placesOfBirth: [],
            enabled: true,
            quitConfirmation: false,
            saveConfirmation: false,
            addressConfirmation: false,
            addressChangedConfirmation: false,
            regionsLoading: false,
            districtsLoading: false,
            citiesLoading: false,
            streetsLoading: false,
            placesOfBirthLoading: false
        }
    },
    props: ["initialPerson", "level"],
    computed: {
        dateOfBirth: {
            get: function () {
                return moment(this.person.dateOfBirth).format('YYYY-MM-DD')
            },
            set: function (data) {
                this.person.dateOfBirth = data;
            }
        },
        districtId: {
            get: function () {
                return this.person.address.districtId || '';
            },
            set: function (data) {
                this.person.address.districtId = data;
            }
        },
        filteredCities: function () {
            return this.cities.filter(c => c.name.toLowerCase().startsWith(this.cityName.toLowerCase()));
        },
        filteredPlacesOfBirth: function () {
            return this.placesOfBirth.filter(c => c.name.toLowerCase().startsWith(this.placeOfBirthName.toLowerCase()));
        }
    },
    methods: {
        close: function () {
            this.enabled = false;
            this.quitConfirmation = true;
        },
        closeConfirmed: function () {
            this.$emit('quit');
        },
        closeQuited: function () {
            this.quitConfirmation = false;
            this.enabled = true;
        },

        save: function () {
            if ($(this.$el).valid()) {
                this.enabled = false;
                this.saveConfirmation = true;
            }
        },
        saveConfirmed: async function () {
            this.saveConfirmation = false;
            let person = this.person.id ? await this.editPerson() : await this.createPerson();
            if (person) {
                this.person = person;
                this.$emit('save', this.person);
            }
        },
        saveQuitted: function () {
            this.saveConfirmation = false;
            this.enabled = true;
        },

        createPerson: async function () {
            this.person.address.id = 0;
            let address = await DATA_CLIENT.checkAddressExists(this.person.address);
            if (address) {
                this.person.address = address;
                this.addressConfirmation = true;
            } else {
                this.person.addressId = 0;
                this.person.address.addressTypeId = 2;
                return await DATA_CLIENT.createPerson(this.person);
            }
        },

        addressConfirmed: async function () {
            this.person.addressId = this.person.address.id;
            this.person = this.person.id ? await DATA_CLIENT.savePerson(this.person) : await DATA_CLIENT.createPerson(this.person);
            this.$emit('save', this.person);
        },
        addressQuitted: function () {
            this.person.addressId = 0;
            this.addressConfirmation = false;
            this.enabled = true;
        },

        editPerson: async function () {
            if (this.addressChanged()) {
                this.addressChangedConfirmation = true;
            } else {
                return await DATA_CLIENT.savePerson(this.person);
            }
        },
        createAddress: async function () {
            this.person.addressId = 0;
            this.person.address.id = 0;
            await this.changeAddress();
        },
        changeAddress: async function () {
            let address = await DATA_CLIENT.checkAddressExists(this.person.address);
            if (address) {
                this.person.address = address;
                this.addressConfirmation = true;
            } else {
                await DATA_CLIENT.savePerson(this.person);
                this.$emit('save', this.person);
            }
        },
        cancelAddressChanged: function () {
            this.addressChangedConfirmation = false;
            this.enabled = true;
        },

        addressChanged: function () {
            return this.initialPerson.address.countryId != this.person.address.countryId
                || this.initialPerson.address.regionId != this.person.address.regionId
                || this.initialPerson.address.districtId != this.person.address.districtId
                || this.initialPerson.address.cityId != this.person.address.cityId
                || this.initialPerson.address.streetId != this.person.address.streetId
                || this.initialPerson.address.building != this.person.address.building
                || this.initialPerson.address.apartment != this.person.address.apartment;
        },
        selectedCountryChanged: async function () {
            this.regions.splice(0);
            this.person.address.regionId = '';
            this.districts.splice(0);
            this.person.address.districtId = '';
            this.cities.splice(0);
            this.cityName = '';
            this.person.address.cityId = '';
            this.streets.splice(0);
            this.person.address.streetId = '';

            if (this.person.address.countryId) {
                this.loadRegions(this.person.address.countryId);
                this.loadCities('country', this.person.address.countryId);
            }
        },
        selectedRegionChanged: async function () {
            this.districts.splice(0);
            this.person.address.districtId = '';
            this.cities.splice(0);
            this.cityName = '';
            this.person.address.cityId = '';
            this.streets.splice(0);
            this.person.address.streetId = '';
            if (this.person.address.regionId) {
                this.loadDistricts(this.person.address.regionId);
                this.loadCities('region', this.person.address.regionId);
            }
        },
        selectedDistrictChanged: async function () {
            this.cities.splice(0);
            this.person.address.cityId = '';
            this.cityName = '';
            this.streets.splice(0)
            if (this.person.address.districtId) {
                this.loadCities('district', this.person.address.districtId);
            } else {
                this.person.address.cityId = '';
                this.cities.splice(0);
                this.loadCities('region', this.person.address.regionId);
            }
        },
        selectedCityChanged: async function () {
            this.streets.splice(0);
            if (this.person.address.cityId == '$reset') {
                this.person.address.cityId = '';
                this.cityName = '';
            } else if (this.person.address.cityId) {
                this.person.address.cityId = '';
                this.loadStreets(this.person.address.cityId);
                let city = this.cities.find(c => c.id == this.person.address.cityId);
                this.person.address.districtId = city.districtId;
                this.person.address.regionId = city.regionId;
                if (this.districts.findIndex(d => d.id == city.districtId) == -1) {
                    this.loadDistricts(city.regionId);
                }
            }
        },
        loadRegions: async function (countryId) {
            this.regionsLoading = true;
            this.regions.push(...await DATA_CLIENT.getRegionsByCountry(countryId));
            this.regionsLoading = false;
        },
        loadDistricts: async function (regionId) {
            this.districtsLoading = true;
            let tempDistricts = await DATA_CLIENT.getDistrictsByRegion(regionId);
            this.districts.push(...tempDistricts);
            this.districtsLoading = false;
        },
        loadCities: async function (source, id) {
            let tempCities = [];
            this.citiesLoading = true;
            if (id) {
                switch (source) {
                    case 'country':
                        tempCities = await DATA_CLIENT.getCitiesByCountry(id);
                        break;
                    case 'region':
                        tempCities = await DATA_CLIENT.getCitiesByRegion(id);
                        break;
                    case 'district':
                        tempCities = await DATA_CLIENT.getCitiesByDistrict(id);
                        break;
                }
            }
            this.cities.push(...tempCities);
            this.citiesLoading = false;
        },
        loadStreets: async function (cityId) {
            this.streetsLoading = true;
            this.streets.push(...await DATA_CLIENT.getStreetsByCity(cityId));
            this.streetsLoading = false;
        },
        loadPlacesOfBirth: async function (countryId) {
            this.placesOfBirthLoading = true;
            this.placesOfBirth.splice(0);
            this.placesOfBirth.push(...await DATA_CLIENT.getCitiesByCountry(countryId));
            this.placesOfBirthLoading = false;
        },
        newPerson: function () {
            return {
                firstName: '',
                familyName: '',
                patronimic: '',
                genderId: '',
                dateOfBirth: moment(new Date()).format('YYYY-MM-DD'),
                countryOfBirthId: '',
                placeOfBirthId: '',
                taxId: '',
                addressId: '',
                address: {
                    id: '',
                    countryId: '',
                    regionId: '',
                    districtId: '',
                    cityId: '',
                    streetId: '',
                    building: '',
                    apartment: '',
                    zip: ''
                }
            }
        }
    },
    watch: {
        'person.countryOfBirthId': function (val) {
            this.placeOfBirthName = "";
            if (this.placesOfBirth.length) {
                this.placesOfBirth.splice(0);
                this.person.placeOfBirthId = '';
            }
            if (val) {
                this.loadPlacesOfBirth(val);
            }
        },
        'person.placeOfBirthId': function (val) {
            if (val == '$reset') {
                this.placeOfBirthName = '';
                this.person.placeOfBirthId = '';
            } else if (!val) {
                this.placeOfBirthName = '';
            }
        },
        'person.address.cityId': function (val) {
            if (val && val!='$reset') {
                this.person.address.city = this.cities.find(c => c.id == val);
            }
        },
        'person.address.streetId': function (val) {
            if (val) {
                this.person.address.street = this.streets.find(s => s.id == val);
            }
        }
    },
    created: async function () {
        if (this.initialPerson) {
            if (this.initialPerson.address) {
                if (this.initialPerson.address.city) {
                    this.cityName = this.initialPerson.address.city.name;
                }
                if (this.initialPerson.address.districtId) {
                    this.loadCities('district', this.initialPerson.address.districtId);
                }
                if (this.initialPerson.address.regionId) {
                    await this.loadDistricts(this.initialPerson.address.regionId)
                    if (!this.cities.length) {
                        this.loadCities('region', this.initialPerson.address.regionId);
                    }
                }
                if (this.initialPerson.address.countryId) {
                    this.loadRegions(this.initialPerson.address.countryId);
                    if (!this.cities.length) {
                        this.loadCities('country', this.initialPerson.address.countryId);
                    }
                }
                if (this.initialPerson.address.cityId) {
                    this.loadStreets(this.initialPerson.address.cityId);
                }
            }
            if (this.initialPerson.countryOfBirthId) {
                await this.loadPlacesOfBirth(this.initialPerson.countryOfBirthId);
                if (this.initialPerson.placeOfBirthId) {
                    this.placeOfBirthName = this.placesOfBirth
                        .find(c => c.id == this.initialPerson.placeOfBirthId).name;
                }
            }
        } else {
            this.person.address.countryId = 71;
            this.loadRegions(71);
            this.loadCities('country', 71);
            this.person.countryOfBirthId = 71;
        }
    },
    mounted: function () {
        $.validator.unobtrusive.parse($(this.$el))
    }
});