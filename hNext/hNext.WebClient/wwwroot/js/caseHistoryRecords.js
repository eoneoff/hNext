"use strict";

Vue.component("CaseHistoryRecords", {
    template: '#case-history-records-template',
    store,
    data: function () {
        return {
            selectedRecord: -1
        };
    },
    methods: {
        selectRecord: function (rec) {
            this.selectedRecord = this.selectedRecord == rec ? -1 : rec;
        }
    }
});