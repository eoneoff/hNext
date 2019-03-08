﻿"use strict";

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
                foundPatients: [],
                searching: false
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
            },
            enabled: function () {
                return store.state.openModals == 0;
            }
        },
        methods: {
            searchPatients: async function () {
                this.searching = true;
                try {
                    store.state.patient = { id: 0 };
                    this.foundPatients.splice(0);
                    this.foundPatients.push(...await DATA_CLIENT.searchPatients(this.model));
                }
                finally {
                    this.searching = false;
                }
            },
            selectPatient: function (patient) {
                if (this.enabled) {
                    this.selectedPatient = patient;
                }
            },
            createNewPatient: function () {
                store.state.patient = { id: 0 };
                store.commit('openModal');

                let modalState = store.state.modals['personEditorModal'] || {};
                modalState.open = true;
                modalState.rootItem = 'patient';

                let closeHandler = function () { };
                if (!modalState.onClose) {
                    modalState.onClose = [closeHandler];
                } else {
                    modalState.onClose.push(closeHandler);
                }

                store.commit('setModalState', { key: 'personEditorModal', value: modalState });
            },
            editPatient: function () {
                store.commit('openModal');

                let modalState = store.state.modals['personEditorModal'] || {};
                modalState.open = true;
                modalState.rootItem = 'patient';

                store.commit('setModalState', { key: 'personEditorModal', value: modalState });
            }
        }
    });
}