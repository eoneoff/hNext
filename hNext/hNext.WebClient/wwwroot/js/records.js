"use strict";

Vue.component("Records", {
    template: '#records-template',
    store,
    data: function () {
        return {
            templates: [],
            selectedRecord: {},
            showTemplateEditor: false,
            showEditConfirmation: false
        };
    },
    methods: {
        newTemplate: function () {
            this.selectedRecord = this.template();
            this.showTemplateEditor = true;
        },
        editTemplateClicked: function () {
            if (this.selectedRecord.id) {
                this.showEditConfirmation = true;
            }
        },
        editTemplate: function () {
            this.showEditConfirmation = false;
            this.showTemplateEditor = true;
        },
        saveTemplate: async function (template) {
            this.showTemplateEditor = false;
            this.templates.push(await DATA_CLIENT.addRecordTemplate(template));
            this.selectedRecord = {};
        },
        template: function () {
            return {
                id: '',
                name: '',
                header: '',
                hospitalId: '',
                departmentId: '',
                specialtyId: '',
                doctorId: '',
                recordFieldTemplates: []
            };
        }
    },
    computed: {
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
        showTemplateEditor: function (val) {
            this.enabled = !val;
        },
        showEditConfirmation: function (val) {
            this.enabled = !val;
        }
    },
    mounted: function () {
        DATA_CLIENT.getRecordTemplates().then(result => this.templates = result);
    }
});