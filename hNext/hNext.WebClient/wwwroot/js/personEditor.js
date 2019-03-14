﻿"use strict";

Vue.component('PersonEditor', {
    template: "#person-editor-template",
    data: function () {
        return {
            person: $.extend(true, this.newPerson(), this.initialPerson),
            regions: [],
            districts: [],
            cities: [],
            streets: [],
            placesOfBirth: [],
            enabled: true,
            quitConfirmation: false,
            saveConfirmation: false,
            addressConfirmation: false,
            addressChangedConfirmation: false
        }
    },
    props:["initialPerson", "level"],
    computed: {
        dateOfBirth: {
            get: function () {
                return moment(this.person.dateOfBirth).format('YYYY-MM-DD')
            },
            set: function (date) {
                this.person.dateOfBirth = Date.parse(date);
            }
        }
    },
    methods: {
        close: function () {
            this.enabled = false;
            this.quitConfirmation = true;
        },
        save: function () {
            if ($(this.$el).valid()) {
                this.enabled = false;
                this.saveConfirmation = true;
            }
        },

        closeConfirmed: function() {
            this.$emit('quit');
        },
        closeQuited: function () {
            this.quitConfirmation = false;
            this.enabled = true;
        },

        saveConfirmed: async function () {
            this.saveConfirmation = false;
            if (!this.person.id) {
                let person = await this.createPerson();
                if (!person) {
                    return;
                } else {
                    this.person = person;
                }
            } else {
                this.person = await this.editPerson();
            }
            this.$emit('save', this.person);
        },
        saveQuitted: function() {
            this.saveConfirmation = false;
            this.enabled = true;
        },

        createPerson: async function() {
            let addressId = DATA_CLIENT.checkAddressExists(this.editPerson.address);
            if (addressId) {
                this.person.addressId = addressId;
                this.confirmAddress();
                return;
            }
            return await DATA_CLIENT.createPerson(person);
        },

        confirmAddress: function () {
            this.addressConfirmation = true;
            this.enabled = false;
        },
        addressConfirmed: async function () {
            this.person.address = {};
            this.person = await DATA_CLIENT.createPerson(person);
            this.$emit('save', this.person);
        },
        addressQuitted: function () {
            this.addressConfirmation = false;
            this.enabled = true;
        },
        
        editPerson: async function() {
            if (this.addressChanged()) {

            }
            this.person = await DATA_CLIENT.savePerson(this.person);
            this.$emit('save', this.person);
        },

        addressChanged: function () {
            return this.initialPerson.address.countryId != this.person.address.countryId
                || this.initialPerson.address.regionId != this.person.address.regionId
                || this.initialPerson.address.districtId != this.person.address.districtId
                || this.initialPerson.address.cityId != this.person.address.cityId
                || this.initialPerson.address.streetId != this.persson.address.streetId
                || this.initialPerson.address.building != this.person.address.building
                || this.initialPerson.address.apartment != this.person.address.apartment;
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
        'person.address.countryId': async function (val) {
            let tempRegions = [];
            let tempCities = [];

            if (val) {
                tempRegions = await DATA_CLIENT.getRegionsByCountry(val);
                tempCities = await DATA_CLIENT.getCitiesByCountry(val);
            }

            if (this.regions.length) {
                this.person.address.regionId = '';
                this.regions.splice(0);
            }
            this.regions.push(...tempRegions);

            if (this.cities.length) {
                this.person.address.cityId = '';
                this.cities.splice(0);
            }
            this.cities.push(...tempCities);
        },
        'person.countryOfBirthId': async function (val) {
            if (this.placesOfBirth.length) {
                this.placesOfBirth.splice(0);
                this.person.placeOfBirthId = '';
            }
            if (val) {
                this.placesOfBirth.push(...await DATA_CLIENT.getCitiesByCountry(val));
            }
        },
        'person.address.regionId': async function (val) {
            let tempDistricts = [];
            let tempCities = [];

            if (val) {
                tempDistricts = await DATA_CLIENT.getDistrictsByRegion(val);
                tempCities = await DATA_CLIENT.getCitiesByRegion(val);
            } else if (this.person.address.countryId) {
                tempCities = await DATA_CLIENT.getCitiesByCountry(this.person.address.countryId);
            }

            if (this.districts.length) {
                this.person.address.districtId = '';
                this.districts.splice(0);
            }
            this.districts.push(...tempDistricts);

            if (this.cities.length) {
                this.person.address.cityId = '';
                this.cities.splice(0);
            }
            this.cities.push(...tempCities);
        },
        'person.address.districtId': async function (val) {
            let tempCities = [];
            
            if (val) {
                tempCities = await DATA_CLIENT.getCitiesByDistrict(val);
            } else {
                if (this.person.address.regionId) {
                    tempCities = await DATA_CLIENT.getCitiesByRegion(this.person.address.regionId);
                } else if(this.person.address.countryId) {
                    tempCities = await DATA_CLIENT.getCitiesByCountry(this.person.address.countryId);
                }
            }

            if (this.cities.length) {
                this.person.address.cityId = '';
                this.cities.splice(0);
            }
            this.cities.push(...tempCities);
        },
        'person.address.cityId': async function (val) {
            if (this.streets.length) {
                this.streets.splice(0);
                this.person.address.streetId = '';
            }
            if (val) {
                this.streets.push(...await DATA_CLIENT.getStreetsByCity(val));
            }

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
    },
    created: async function () {
        if (this.initialPerson) {
            if (this.initialPerson.address) {
                if (this.initialPerson.address.districtId) {
                    this.cities.push(... await DATA_CLIENT.getCitiesByDistrict(this.initialPerson.address.districtId));
                }
                if (this.initialPerson.address.regionId) {
                    this.districts.push(...await DATA_CLIENT.getDistrictsByRegion(this.initialPerson.address.regionId));
                    if (!this.cities.length) {
                        this.cities.push(...await DATA_CLIENT.getCitiesByRegion(this.initialPerson.address.regionId));
                    }
                }
                if (this.initialPerson.address.countryId) {
                    this.regions.push(...await DATA_CLIENT.getRegionsByCountry(this.initialPerson.address.countryId));
                    if (!this.cities.length) {
                        this.cities.push(...await DATA_CLIENT.getCitiesByCountry(this.initialPerson.address.countryId));
                    }
                }
                if (this.initialPerson.address.cityId) {
                    this.streets.push(...await DATA_CLIENT.getStreetsByCity(this.initialPerson.address.cityId));
                }
            }
            if (this.initialPerson.countryOfBirthId) {
                this.placesOfBirth.push(...await DATA_CLIENT.getCitiesByCountry(this.initialPerson.countryOfBirthId));
            }
        }
    },
    mounted: function () {
        $.validator.unobtrusive.parse($(this.$el))
    }
});