"use strict";

Vue.component("PatientAdditionalData", {
    template: '#patient-additional-data-template',
    methods: {
        addEmailToPerson: async function (email) {
            email.personId = store.state.patient.personId;
            return await DATA_CLIENT.addEmailToPerson(email);
        },
        deleteEmailFromPerson: async function (email) {
            email.personId = store.state.patient.personId;
            return await DATA_CLIENT.deleteEmailFromPerson(email);
        },
        addPhoneToPerson: async function (phone) {
            phone.personId = store.state.patient.personId;
            return await DATA_CLIENT.addPhoneToPerson(phone);
        },
        deletePhoneFromPerson: async function (phone) {
            phone.personId = store.state.patient.personId;
            return await DATA_CLIENT.deletePhoneFromPerson(phone);
        }
    },
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
    },
    provide: function () {
        return {
            addEmail: this.addEmailToPerson,
            deleteEmail: this.deleteEmailFromPerson,
            addPhone: this.addPhoneToPerson,
            deletePhone: this.deletePhoneFromPerson
        }
    }
});