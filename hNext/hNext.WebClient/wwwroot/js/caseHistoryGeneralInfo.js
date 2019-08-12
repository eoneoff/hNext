"use strict";

Vue.component("CaseHistoryGeneralInfo", {
    template: '#case-history-general-info-template',
    props:['level'],
    store,
    data: function () {
        return {
            selectedDiagnosys: { diagnosysId: 0 },
            selectedAdmission: {id:0},
            diagnosysTypes: DIAGNOSYS_TYPE,
            diagnosysWhenSet: WHEN_SET,
            showDiagnosysEditor: false,
            showDiagnosysDeleteConfirmation: false,
            showAdmissionEditor:false
        };
    },
    computed: {
        history: function () {
            return this.$store.state.caseHistory.history;
        },
        diagnoses: function () {
            return (this.history.diagnoses || []).filter(d => d.active);
        },
        admissions: function () {
            return (this.$store.state.caseHistory.history.admissions || []).sort((a, b) => 
                a ? b ? a.getTime() - b.getTime() : -1 : 1 );
        },
        enabled:{
            get: function(){
                return this.$store.state.enabled;
            },
            set: function (val) {
                this.$store.commit('enable', val);
            }
        }
    },
    methods: {
        selectDiagnosys: function (diagnosys) {
            if (this.enabled) {
                this.selectedDiagnosys = diagnosys;
            }
        },
        selectAdmission: function (admission) {
            if (this.enabled) {
                this.selectedAdmission = admission;
            }
        },
        admissionStartDate: function (index) {
            return index ? this.admissions[index = 1].discharged : this.history.startDate;
        },
        addDiagnosys: function () {
            this.showDiagnosysEditor = true;
        },
        saveDiagnosys: async function (diagnosys) {
            this.showDiagnosysEditor = false;
            diagnosys.caseHistoryId = this.history.id;
            if (this.diagnoses.findIndex(d => d.diagnosysId == diagnosys.diagnosysId) == -1) {
                this.$store.commit('setDiagnosys', await DATA_CLIENT.addDiagnosysToCaseHistory(diagnosys));
            }
        },
        removeDiagnosys: function () {
            if (this.selectedDiagnosys.diagnosysId) {
                this.showDiagnosysDeleteConfirmation = true;
            }
        },
        removeConfirmed: function () {
            this.showDiagnosysDeleteConfirmation = false;
            this.selectedDiagnosys.active = false;
            DATA_CLIENT.removeDiagnosysFromCaseHistory(this.selectedDiagnosys);
            this.selectedDiagnosys = { diagnosysId: 0 };
        },
        addAdmission: async function (admission) {
            admission.hospitalId = this.history.hospitalId;
            admission.caseHistoryId = this.history.id;
            this.$store.commit('addAdmission', await DATA_CLIENT.addAdmissionToCaseHistory(admission));
        }
    },
    watch: {
        showDiagnosysEditor: function (val) {
            this.enabled = !val;
        },
        showDiagnosysDelecteConfirmation: function (val) {
            this.enabled = !val;
        },
        showAdmissionEditor: function (val) {
            this.enabled = !val;
        }
    }
});