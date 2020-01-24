"use strict";

Vue.component("CaseHistoryRecords", {
    template: '#case-history-records-template',
    store,
    data: function () {
        return {
            newRecord: {},
            recordAddMode: false,
            selectedTemplate: {},
            selectedTemplateId: '',
            templateLoading: false,
            cancelAddRecordConfirmation: false,
            openedRecords:[],
            recordId:0
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
    watch: {
        cancelAddRecordConfirmation: function (val) {
            this.enabled = !val;
        }
    },
    methods: {
        clickRecord: function (id) {
            const index = this.openedRecords.indexOf(id);
            if (index == -1) {
                this.openedRecords.push(id);
            } else {
                this.openedRecords.splice(index, 1);
            }
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
                else {
                    this.selectedTemplate = '';
                }
                this.recordAddMode = true;
            }
        },
        addRecord: function (record) {
            record.id = this.recordId++;
            this.$store.commit('addRecord', record);
            this.recordAddMode = false;
        }
    }
});