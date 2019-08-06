"use strict";

Vue.component('CaseHistoryDiagnosysEditor', {
    template: '#case-history-diagnosys-editor-template',
    props:['level', 'initialEnabled'],
    data: function () {
        return {
            enabled: true,
            showDiagnosysEditor: false,
            diagnosys: {
                diagnosysId: '',
                caseHistoryId: this.caseHistoryId,
                active: true,
                date: moment(new Date).format('YYYY-MM-DD'),
                whenSet: '',
                type:''
            },
            showSaveConfirmation: false,
            showCancelConfirmation: false
        };
    },
    computed: {
        caseHistoryId: function () {
            return this.$store.state.caseHistory.history.id;
        },
        patientId: function () {
            return this.$store.state.caseHistory.history.patientId;
        },
        diagnoses: function () {
            return (this.$store.state.caseHistory.history.patient || {}).diagnoses || [];
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
            this.$store.commit('addDiagnosysToPatient', await DATA_CLIENT.addDiagnosysToPatient(diagnosys));
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
    mounted: async function() {
        $.validator.unobtrusive.parse($(this.$el));
    }
});