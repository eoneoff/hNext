// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict";

class DataClient {

    constructor(apiServer) {
        this._apiServer = apiServer;
    }

    async getRegionsByCountry(countryId) {
        return (await axios.get(`${this._apiServer}countries/${countryId}/regions`)).data;
    }

    async getCitiesByCountry(countryId) {
        return (await axios.get(`${this._apiServer}countries/${countryId}/cities`)).data;
    }

    async getDistrictsByRegion(regionId) {
        return (await axios.get(`${this._apiServer}regions/${regionId}/districts`)).data;
    }

    async getCitiesByRegion(regionId) {
        return (await axios.get(`${this._apiServer}regions/${regionId}/cities`)).data;
    }

    async getCitiesByDistrict(districtId) {
        return (await axios.get(`${this._apiServer}districts/${districtId}/cities`)).data;
    }

    async getStreetsByCity(cityId) {
        return (await axios.get(`${this._apiServer}cities/${cityId}/streets`)).data;
    }

    async searchPatients(model) {
        return (await axios.post(
            `${this._apiServer}patients/search`,
            model
        )).data;
    }
}

const DATA_CLIENT = new DataClient(API_SERVER);

Vue.use(Vuex);

const store = new Vuex.Store({
    state: {
        patient: { id: 0 },
        enabled: true
    },
    mutations: {
        setPatient(state, patient) {
            state.patient = patient;
        },
        enable(state, show) {
            state.enabled = show;
        }
    }
});

Vue.filter('shortDate', (date) => {
    if (moment(date).isValid()) {
        let locale = window.navigator.userLanguage || window.navigator.language;
        moment.locale(locale);
        return moment(date).format('L');
    } else return '';
});