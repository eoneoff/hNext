"use strict";

if (!store.state.diagnoses) {
    const diagnosysModule = {
        state: {
            diagnoses: []
        },
        mutations: {
            setDiagnoses: function (state, diagnoses) {
                state.diagnoses = diagnoses.sort((a, b) => a.name.localeCompare(b.name));
            },
            addDiagnosys: function (state, diagnosys) {
                let i = -1;
                while (++i < state.diagnoses.length && state.diagnoses[i].localeCompare(diagnosys.name) <= 0);
                state.diagnoses.splice(i, 0, diagnosys);
            }
        },
        actions: {
            loadDiagnoses: async function (context) {
                context.commit('setDiagnoses', await DATA_CLIENT.getDiagnoses());
            }
        }
    };
    store.registerModule('diagnoses', diagnosysModule);
    store.dispatch('loadDiagnoses');
}

Vue.component("PatientDiagnosysEditor", {
    template: '#patient-diagnosys-editor-template',
    props: ['level', 'initialEnabled', 'patientId'],
    data: function () {
        return {
            enabled: true,
            showDiagnosysEditor: false,
            showSaveConfirmation: false,
            showCancelConfirmation:false,
            diagnosys: {
                active: true,
                patientId: this.patientId,
                diagnosysId: '',
                date: moment(new Date()).format('YYYY-MM-DD')
            }
        };
    },
    computed: {
        diagnoses: function () {
            return this.$store.state.diagnoses.diagnoses;
        }
    },
    methods: {
        save: function () {
            if ($(this.$el).valid()) {
                this.showSaveConfirmation = true;
            }
        },
        cancel: function () {
            this.showCancelConfirmation = true;
        },
        addDiagnosys: async function (diagnosys) {
            this.showDiagnosysEditor = false;
            this.$store.commit('addDiagnosys', await DATA_CLIENT.addDiagnosys(diagnosys));
        },
        getICD: function (diagnosys) {
            let icd = diagnosys.icd.letter;
            icd = icd + (diagnosys.icd.primaryNumber < 10 ? `0${diagnosys.icd.primaryNumber}` : diagnosys.icd.primaryNumber);
            icd = icd + `.${diagnosys.icd.secondaryNumber}`;
            return icd;
        }
    },
    watch: {
        showDiagnosysEditor: function (val) {
            this.enabled = !val;
        },
        showSaveConfirmation: function (val) {
            this.enabled = !val;
        },
        showCancelConfirmation: function (val) {
            this.enabled = !val;
        }
    },
    mounted: function () {
        $.validator.unobtrusive.parse($(this.$el));
    }
});