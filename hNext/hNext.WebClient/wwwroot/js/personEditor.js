"use strict";

function personEditorModel(id, level = 1) {
    return new Vue({
        el: `#${id}`,
        data: function () {
            return {
                person: {
                    firstName: '',
                    familyName: '',
                    patronimic: '',
                    genderId:0,
                    dateOfBirth: (new Date()).getDate(),
                    countryOfBirthId: 0,
                    placeOfBirthId: '',
                    taxId: '',
                    addressId: '',
                    gender: {
                        id: 0,
                        name:''
                    },
                    countryOfBirth: {
                        id: 0,
                        name: ''
                    },
                    placeOfBirth: {
                        id: 0,
                        name:''
                    },
                    address: {
                        id: 0,
                        countryId: 0,
                        regionId: 0,
                        districtId: 0,
                        cityId: 0,
                        streetId: 0,
                        building: '',
                        apartment: '',
                        zip: '',
                        country: {
                            id: 0,
                            name:''
                        },
                        region: {
                            id: 0,
                            name:''
                        },
                        district: {
                            id: 0,
                            name:''
                        },
                        city: {
                            id: 0,
                            name: '',
                            cityTypeId: 0,
                            cityType: {
                                id: 0,
                                name:''
                            }
                        },
                        street: {
                            id: 0,
                            name: '',
                            streetTypeId: 0,
                            streetType: {
                                id: 0,
                                name:''
                            }
                        }
                    }
                },
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
                    return store.state.modals[this.$el.id];
                },
                set: function(display) {
                    store.commit('setModalState', {key:this.$el.id, value:display});
                }
            },
            enabled: function() {
                return store.state.openModals = this.modalLevel;
            }
        },
        methods: {
            close: function () {
                store.commit('closeModal');
                this.display = false;
            }
        },
        created: function () {
            if (store.state.patient.id != 0) {
                this.person = store.state.patient.person;
            }
        },
        watch: {
            'person.address.countryId': async function (val) {
                this.regions.splice(0);
                this.cities.splice(0);
                if (val != '') {
                    this.regions.push(...await DATA_CLIENT.getRegionsByCountry(val));
                    this.cities.push(...await DATA_CLIENT.getCitiesByCountry(val);
                }
                this.person.address.regionId = '';
                this.person.address.cityId = '';
            },
            'person.address.regionId': async function (val) {
                this.districts.splice(0);
                this.cities.splice(0);
                if (val != '') {
                    this.districts.push(...await DATA_CLIENT.getDistrictsByRegion(val));
                    this.cities.push(...await DATA_CLIENT.getCitiesByRegion(val));
                }
                this.person.address.districtId = '';
                this.person.address.cityId = '';
            },
            'person.address.districtId': async function (val) {
                this.cities.splice(0);
                if (val != '') {
                    this.cities.push(...await DATA_CLIENT.getCitiesByDistrict(val));
                }
                this.person.address.cityId = '';
            },
            'person.address.cityId': async function () {
                this.streets.splice(0);
                if (val != '') {
                    this.street.push(...await DATA_CLIENT.getStreetsByCity(val));
                }
                this.person.address.streetId = '';
            }
        }
    });
}