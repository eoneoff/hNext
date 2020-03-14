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
            openedRecords:[]
        };
    },
    computed: {
        records: function () {
            return (this.$store.state.caseHistory.history.records || []).sort((r1, r2) => r1.date - r2.date);
        },
        rawDiagnoses: function () {
            return this.$store.state.caseHistory.history.diagnoses.sort((d1, d2) => d1.type - d2.type).map(d => d.diagnosys);
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
        },
        records: function (newList, oldList) {
            if (!oldList.length) {
                this.selectedTemplateId = newList[newList.length - 1].recordTemplateId;
            }
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
        addRecordClicked: async function () {
            if (this.selectedTemplateId) {
                this.enabled = false;
                try {
                    if (this.selectedTemplateId > 0) {
                        const template = (this.records.find(r => r.templateId == this.selectedTemplateId) || {}).recordTemplate;
                        if (template) {
                            this.selectedTemplate = template;
                        } else {
                            this.templateLoading = true;
                            this.selectedTemplate = await DATA_CLIENT.getRecordTemplate(this.selectedTemplateId);
                            this.templateLoading = false;
                        }
                    }
                    else {
                        this.selectedTemplate = '';
                    }
                } finally {
                    this.enabled = true;
                }
                
                this.recordAddMode = true;
            }
        },
        saveRecord: function (record) {
            this.recordAddMode = false;
            this.$store.dispatch('saveRecord', record);
        },
        deleteRecord: async function (record) {
            this.enabled = false;
            try {
                this.$store.dispatch('removeRecord', record);
            } finally {
                this.enabled = false;
            }
            
        },
        saveDiagnosys: function (diagnosys, close) {
            close();
            if (diagnosys.id) {

            } else {

            }
        }
    },
    mounted: function () {
        if (this.records.length) {
            this.selectedTemplateId = this.records[this.records.length - 1].recordTemplateId;
        }
    }
});