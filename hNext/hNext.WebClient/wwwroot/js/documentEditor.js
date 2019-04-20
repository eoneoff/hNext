"use strict";

Vue.component("DocumentEditor", {
    template: '#document-editor-template',
    props: ['initialDocument', 'level'],
    data: function () {
        return {
            document: $.extend(true, {}, this.initialDocument),
            enabled: true,
            showSaveConfirmation: false,
            showChangeConfirmation: false,
            showExisting: false
        }
    },
    methods: {
        save: async function () {
            if ($(this.$el).valid()) {
                this.enabled = false;
                if (await DATA_CLIENT.documentExists(this.document)) {
                    this.showExisting = true;
                } else if (this.document.id) {
                    this.showChangeConfirmation = true;
                } else {
                    this.showSaveConfirmation = true;
                }
            }
        },
        saveConfirmed: async function () {
            this.$emit('save', this.document);
        },
        saveCancelled: function () {
            this.showSaveConfirmation = false;
            this.showChangeConfirmation = false;
            this.enabled = true;
        },
        existingClicked: function () {
            this.showExisting = false;
            this.enabled = true;
        }
    },
    computed: {
        dateOfIssue: {
            get: function () {
                return moment(this.document.dateOfIssue).format('YYYY-MM-DD');
            },
            set: function (data) {
                this.document.dateOfIssue = data;
            }
        }
    },
    mounted: function () {
        $.validator.unobtrusive.parse($(this.$el));
    }
});