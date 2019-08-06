"use strict";

if (!store.state['caseHistory']) {
    const historyModule = {
        state: {
            history: {}
        },
        mutations: {
            setHistory(state, history) {
                state.history = history;
            },
            setDiagnosys(state, diagnosys) {
                state.history.diagnoses.push(diagnosys);
            },
            addDiagnosysToPatient(state, diagnosys) {
                state.history.patient.diagnoses.push(diagnosys);
            },
            setDiagnosysInactive(state, diagnosysId) {
                let index = state.history.diagnoses.findIndex(d => d.diagnosysId == diagnosysId);
                let diagnosis = state.history.diagnoses[index];
                diagnosis.active = false;
                Vue.set(state.history.diagnoses, index, diagnosys);
            }
        }
    };
    store.registerModule("caseHistory", historyModule);
}

DATA_CLIENT.getCaseHistory(CASE_HISTORY_ID).then(result => 
    store.commit('setHistory', result));

Vue.component("CaseHistoryMainView", {
    template: '#case-history-main-view-template',
    store,
    data: function () {
        return {
            selectedTab: START_TAB
        }
    },
    computed: {
        history: function () {
            return this.$store.state.caseHistory.history;
        },
        enabled: {
            get: function () {
                return this.$store.state.enabled;
            },
            set: function (val) {
                this.$store.commit('enable', val);
            }
        }
    },
    methods: {
        tabClicked: function (tabName) {
            if (this.enabled) {
                this.selectedTab = tabName;
            }
        }
    }
});