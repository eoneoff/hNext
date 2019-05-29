"use strict";

Vue.component("PatientAdditionalData", {
    template: '#patient-additional-data-template',
    store,
    methods: {
        addEmailToPerson: async function (email) {
            email.personId = this.personId;
            return await DATA_CLIENT.addEmailToPerson(email);
        },
        deleteEmailFromPerson: async function (email) {
            email.personId = this.personId;
            return await DATA_CLIENT.deleteEmailFromPerson(email);
        },
        addPhoneToPerson: async function (phone) {
            phone.personId = this.personId;
            return await DATA_CLIENT.addPhoneToPerson(phone);
        },
        deletePhoneFromPerson: async function (phone) {
            phone.personId = this.personId;
            return await DATA_CLIENT.deletePhoneFromPerson(phone);
        }
    },
    computed: {
        personId: function () {
            return ((this.$store.state.patient.patient || {}).person || {}).id;
        },
        phones: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person)
                ? this.$store.state.patient.patient.person.phones : [];
        },
        emails: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person)
                ? this.$store.state.patient.patient.person.emails : [];
        },
        documents: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person)
                ? this.$store.state.patient.patient.person.documents : [];
        },
        guardians: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person)
                ? this.$store.state.patient.patient.person.guardians : [];
        },
        enabled: {
            get: function () {
                return this.$store.state.enabled;
            },
            set: function (show) {
                this.$store.commit('enable', show);
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