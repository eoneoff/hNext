"use strict";

function patientSearchModel(id) {
    return new Vue({
        el: `#${id}`,
        data: function () {
            return {
                model: {
                    name: "",
                    yearOfBirth: "",
                    regionId: "",
                    districtId: "",
                    cityId: ""
                },
                districts: [],
                cities: [],
                foundPatients: []
            }
        },
        watch: {
            'model.regionId': async function (val) {
                this.districts.splice(0);
                this.cities.splice(0);
                if (val != "") {
                    this.districts.push(...await DATA_CLIENT.getDistrictsByRegion(val));
                    this.cities.push(...await DATA_CLIENT.getCitiesByRegion(val));
                }
                this.model.districtId = "";
                this.model.cityId = "";
            },
            'model.districtId': async function (val) {
                this.cities.splice(0);
                if (val != "") {
                    this.cities.push(...await DATA_CLIENT.getCitiesByDistrict(val));
                }
                this.model.cityId = "";
            }
        },
        computed: {
            selectedPatient: {
                get: function () {
                    return store.state.patient;
                },
                set: function (patient) {
                    store.commit('setPatient', patient);
                }
            }
        },
        methods: {
            searchPatients: async function () {
                store.state.patient = { id: 0 };
                this.foundPatients.splice(0);
                this.foundPatients.push(...await DATA_CLIENT.searchPatients(this.model));
            }
        }
    });
}