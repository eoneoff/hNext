"use strict";

function personEditorModel(id, level = 1) {
    return new Vue({
        el: `#${id}`,
        data: function () {
            return {
                person: this.newPerson(),
                regions: [],
                districts: [],
                cities: [],
                streets: [],
                placesOfBirth:[],
                modalLevel: level
            }
        },
        computed: {
            display: {
                get: function () {
                    return (store.state.modals[(this.$el || {}).id] || {})['open'];
                },
                set: function (display) {
                    let modalState = store.state.modals[this.$el.id];
                    modalState['open'] = display;
                    store.commit('setModalState', {key:this.$el.id, value:modalState});
                }
            },
            dateOfBirth: {
                get: function () {
                    return moment(this.person.dateOfBirth).format('YYYY-MM-DD')
                },
                set: function (date) {
                    person.dateOfBirth = Date.parse(date);
                }
            },
            enabled: function() {
                return store.state.openModals = this.modalLevel;
            }
        },
        methods: {
            close: function () {
                let closeHandlers = store.state.modals[this.$el.id].onClose;
                if (closeHandlers) {
                    for (let handler of closeHandlers) {
                        handler();
                    }
                }
                this.regions.splice(0);
                this.districts.splice(0);
                this.cities.splice(0);
                this.placesOfBirth.splice(0);
                this.streets.splice(0);
                store.commit('closeModal');
                this.display = false;
            },
            newPerson: function () {
                return {
                    firstName: '',
                    familyName: '',
                    patronimic: '',
                    genderId: '',
                    dateOfBirth: new Date(),
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
            display: function (val) {
                if (val && store.state.patient.id != 0) {
                    this.person = $.extend(true, {}, store.state.patient.person);
                } else {
                    this.person = this.newPerson();
                }
            },
            'person.address.countryId': async function (val) {
                let reg = [];
                let cit = []
                if (this.regions.length) {
                    this.person.address.regionId = '';
                }
                if (this.cities.length) {
                    this.person.address.cityId = '';
                }
                if (val) {
                    reg = await DATA_CLIENT.getRegionsByCountry(val);
                    cit = await DATA_CLIENT.getCitiesByCountry(val);
                }

                this.regions.splice(0);
                this.regions.push(...reg);
                this.cities.splice(0);
                this.cities.push(...cit);

                this.person.address.regionId = this.person.address.regionId || '';
                this.person.address.cityId = this.person.address.cityId || '';
            },
            'person.countryOfBirthId': async function (val) {
                if (this.placesOfBirth.length) {
                    this.placesOfBirth.splice(0);
                    this.person.placeOfBirthId = '';
                }
                if (val) {
                    this.placesOfBirth.push(...await DATA_CLIENT.getCitiesByCountry(val));
                }

                this.person.placeOfBirthId = this.person.placeOfBirthId || '';
            },
            'person.address.regionId': async function (val) {
                let dist = [];
                let cit = [];
                if (this.districts.length) {
                    this.person.address.districtId = '';
                }
                if (this.cities.length) {
                    this.person.address.cityId = '';
                }
                if (val != 0) {
                    dist = await DATA_CLIENT.getDistrictsByRegion(val);
                    cit = await DATA_CLIENT.getCitiesByRegion(val);
                }

                this.districts.splice(0);
                this.districts.push(...dist);
                this.cities.splice(0);
                this.cities.push(...cit);

                this.person.address.districtId = this.person.address.districtId || '';
                this.person.address.cityId = this.person.address.cityId || '';
            },
            'person.address.districtId': async function (val) {
                let cit = [];
                if (this.cities.length) {
                    this.person.address.cityId = '';
                }
                if (val) {
                    cit = await DATA_CLIENT.getCitiesByDistrict(val);
                }

                this.cities.splice(0);
                this.cities.push(...cit);

                this.person.address.cityId = this.person.address.cityId || '';
            },
            'person.address.cityId': async function (val) {
                if (this.streets.length) {
                    this.streets.splice(0);
                    this.person.address.streetId = '';
                }
                if (val) {
                    this.streets.push(...await DATA_CLIENT.getStreetsByCity(val));                    
                }

                this.person.address.streetId = this.person.address.streetId || '';

                if (!this.person.address.city
                    || this.person.address.city.id != this.person.address.cityId) {
                    this.person.address.city = this.cities.find(city => city.id == val);
                }
            },
            'person.address.streetId': function (val) {
                if (val) {
                    if (!this.person.address.street
                        || this.person.address.street.id != this.person.address.streetId) {
                        this.person.address.street = this.streets.find(street => street.id == val);
                    }
                }
            }
        }
    });
}