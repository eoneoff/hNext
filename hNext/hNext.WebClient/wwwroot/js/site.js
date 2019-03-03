// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict";

class DataClient {

    constructor(apiServer) {
        this._apiServer = apiServer;
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

    async searchPatients(model) {
        return (await axios({
            method: 'post',
            url: `${this._apiServer}patients/search`,
            data: model
        }
        )).data;
    }
}

const DATA_CLIENT = new DataClient(API_SERVER);

const store = new Vuex.Store({
    state: {
        patient: { id:0 }
    },
    mutations: {
        setPatient(state, patient) {
            state.patient = patient;
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