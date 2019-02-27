// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict"

class DataClient {

    constructor(apiServer) {
        this.apiServer = apiServer;
    }

    async getDistrictsByRegion(regionId) {
        return (await axios.get(`${this.apiServer}regions/${regionId}/districts`)).data;
    }

    async getCitiesByRegion(regionId) {
        return (await axios.get(`${this.apiServer}regions/${regionId}/cities`)).data;
    }

    async getCitiesByDistrict(districtId) {
        return (await axios.get(`${this.apiServer}districts/${districtId}/cities`)).data;
    }

    async searchPatients(model) {
        return (await axios({
            method: 'post',
            url: `${this.apiServer}patients/search`,
            data: model
        })).data;
    }
}

const DATA_CLIENT = new DataClient(API_SERVER);

Vue.filter('shortDate', (date) => {
    if (moment(date).isValid()) {
        let locale = window.navigator.userLanguage || window.navigator.language;
        moment.locale(locale);
        return moment(date).format('L');
    } else return '';
});