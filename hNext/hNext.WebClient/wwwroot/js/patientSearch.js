"use string"

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
                if (val != "") {
                    this.districts.splice(0);
                    this.districts.push(...await DATA_CLIENT.getDistrictsByRegion(val));
                    this.model.districtId = "";
                    this.cities.splice(0);
                    this.cities.push(...await DATA_CLIENT.getCitiesByRegion(val));
                    this.model.cityId = "";
                }
            },
            'model.districtId': async function (val) {
                this.cities.splice(0);
                this.cities.push(...await DATA_CLIENT.getCitiesByDistrict(val));
                this.model.cityId = "";
            }
        },
        methods: {
            searchPatients: async function () {
                this.foundPatients.splice(0);
                this.foundPatients.push(...await DATA_CLIENT.searchPatients(this.model));
            }
        }
    });
}