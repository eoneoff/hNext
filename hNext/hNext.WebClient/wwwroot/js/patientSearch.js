"use string"

function patientSearchModel(id) {
    return new Vue({
        el: `#${id}`,
        data: function () {
           return {
               name: "",
               yearOfBirth: "",
               regionId: "",
               districtId: "",
               cityId: "",
               districts: [],
               cities: []
            }
        },
        watch: {
            regionId: async function (val) {
                if (val != "") {
                    this.districts.splice(0);
                    this.districts.push(...await DATA_CLIENT.getDistrictsByRegion(val));
                    this.districtId = "";
                    this.cities.splice(0);
                    this.cities.push(...await DATA_CLIENT.getCitiesByRegion(val));
                    this.cityId = "";
                }
            },
            districtId: async function (val) {
                this.cities.splice(0);
                this.cities.push(...await DATA_CLIENT.getCitiesByDistrict(val));
                this.cityId = "";
            }
        }
    });
}