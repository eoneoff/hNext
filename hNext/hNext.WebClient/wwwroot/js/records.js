"use strict";

Vue.component("Records", {
    template: '#records-template',
    store,
    data: function () {
        return {
            templates: [],
            selectedRecord: {},
            showTemplateEditor: false,
            showEditConfirmation: false,
            dummy:0
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
        saveTemplate: function (template) {
            this.showTemplateEditor = false;
            template.id = ++this.dummy;
            this.selectedRecord = {};
            this.templates.push(template);
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
    }
});