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
        }
    }
});