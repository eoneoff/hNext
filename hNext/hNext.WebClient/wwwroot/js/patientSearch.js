"use string"

function patientSearchModel(id) {
    return new Vue({
        el: `#${id}`,
        data: {
            name: "",
            yearOfBirth: "",
            regionId: "",
            districtId: "",
            cityId: "",
            districts: [],
            cities: []
        }
    });
}