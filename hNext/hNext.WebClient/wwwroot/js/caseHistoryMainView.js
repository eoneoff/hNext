"use strict";

if (!store.state['caseHistory']) {
    const historyModule = {
        state: {
            history: {}
        },
        mutations: {
            setHistory(state, history) {
                state.history = history;
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