"use strict";

Vue.component("CaseHistoryRecords", {
    template: '#case-history-records-template',
    store,
    data: function () {
        return {
            selectedRecord: {},
            newRecord: {},
            recordAddMode: false,
            selectedTemplate: {},
            selectedTemplateId: "",
            templateLoading:false
        };
    },
    computed: {
        records: function () {
            return this.$store.state.caseHistory.history.records;
        },
        patientId: function () {
            return this.$store.state.caseHistory.patientId;
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
        selectRecord: function (record) {
            this.selectedRecord = this.selectedRecord.id == record.id ? {} : record;
        },
        saveRecord: function (record) {
            this.$store.commit('addRecord', record);
        },
        addRecordClicked: async function () {
            if (this.selectedTemplateId) {
                if (this.selectedTemplateId > 0) {
                    this.templateLoading = true;
                    this.selectedTemplate = await DATA_CLIENT.getRecordTemplate(this.selectedTemplateId);
                    this.templateLoading = false;
                }
                this.recordAddMode = true;
            }
        }
    }
});