﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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

    async getCitiesByName(id, starts) {
        return (await axios.get(`${this._apiServer}countries/${id}/byname/${starts}`)).data;
    }

    async getDistrictsByRegion(regionId) {
        return (await axios.get(`${this._apiServer}regions/${regionId}/districts`)).data;
    }

    async getCitiesByRegion(regionId) {
        return (await axios.get(`${this._apiServer}regions/${regionId}/cities`)).data;
    }

    async citiesStartWith(start) {
        return (await axios.get(`${this._apiServer}cities/startswith/${start}`)).data;
    }

    async getStreetsByCity(cityId) {
        return (await axios.get(`${this._apiServer}cities/${cityId}/streets`)).data;
    }

    async checkAddressExists(address) {
        return (await axios.post(
            `${this._apiServer}addresses/exists`,
            address
        )).data;
    }

    async createPerson(person) {
        return (await axios.post(
            `${this._apiServer}people`,
            person
        )).data;
    }

    async savePerson(person) {
        return (await axios.put(
            `${this._apiServer}people/${person.id}`,
            person
        )).data;
    }

    async checkPersonExists(person) {
        return (await axios.post(
            `${this._apiServer}people/exists`,
            person
        )).data;
    }

    async savePatient(patient) {
        return (await axios.post(
            `${this._apiServer}patients`,
            patient
        )).data;
    }

    async editPatient(patient) {
        return (await axios.put(
            `${this._apiServer}patients/${patient.id}`,
            patient
        )).data;
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