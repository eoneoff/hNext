"use strict";

Vue.component("CaseHistoryGeneralInfo", {
    template: '#case-history-general-info-template',
    props:['level'],
    store,
    data: function () {
        return {
            selectedDiagnosys: { diagnosysId: 0 },
            diagnosysTypes: DIAGNOSYS_TYPE,
            diagnosysWhenSet: WHEN_SET,
            showDiagnosysEditor: false
        };
    },
    computed: {
        history: function () {
            return this.$store.state.caseHistory.history;
        },
        diagnoses: function () {
            return (this.history.diagnoses || []).filter(d => d.active);
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
        addDiagnosys: function () {
            this.showDiagnosysEditor = true;
        },
        saveDiagnosys: async function (diagnosys) {
            this.showDiagnosysEditor = false;
            diagnosys.caseHistoryId = this.history.id;
            if (this.diagnoses.findIndex(d => d.diagnosysId == diagnosys.diagnosysId) == -1) {
                this.$store.commit('setDiagnosys', await DATA_CLIENT.addDiagnosysToCaseHistory(diagnosys));
            }
        }
    },
    watch: {
        showDiagnosysEditor: function (val) {
            this.enabled = !val;
        }
    }
});