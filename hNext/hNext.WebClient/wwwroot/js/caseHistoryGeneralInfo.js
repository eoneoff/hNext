"use strict";

Vue.component("CaseHistoryGeneralInfo", {
    template: '#case-history-general-info-template',
    props:['level', 'initialEnabled'],
    store,
    data: function () {
        return {
            enabled:this.initialEnabled
        }
    },
    computed: {
        history: function () {
            return this.$store.state.caseHistory.history;
        }
    }
});