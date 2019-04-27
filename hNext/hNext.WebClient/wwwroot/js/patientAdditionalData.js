"use strict";

Vue.component("PatientAdditionalData", {
    template: '#patient-additional-data-template',
    computed: {
        personId: function () {
            return ((store.state.patient || {}).person || {}).id;
        },
        phones: function () {
            return (store.state.patient && store.state.patient.person)
                ? store.state.patient.person.phones : [];
        },
        emails: function () {
            return (store.state.patient && store.state.patient.person)
                ? store.state.patient.person.emails : [];
        },
        documents: function () {
            return (store.state.patient && store.state.patient.person)
                ? store.state.patient.person.documents : [];
        },
        guardians: function () {
            return (store.state.patient && store.state.patient.person)
                ? store.state.patient.person.guardians : [];
        },
        enabled: {
            get: function () {
                return store.state.enabled;
            },
            set: function (show) {
                store.commit('enable', show);
            }
        }
    }
});