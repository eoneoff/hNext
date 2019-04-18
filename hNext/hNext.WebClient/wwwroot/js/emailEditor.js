"use strict";

Vue.component("EmailEditor", {
    template: '#email-editor-template',
    props: ['initialEmail', 'level'],
    data: function () {
        return {
            email: $.extend(true, {}, this.initialEmail),
            enabled: true,
            showSaveConfirmation: false,
            showChangeConfirmation: false,
            showExistingConfirmation: false,
            showBelongsToOthers: false
        }
    },
    methods: {
        save: function () {
            if ($(this.$el).valid()) {
                this.enabled = false;
                if (this.email.emailId) {
                    this.showChangeConfirmation = true;
                } else {
                    this.showSaveConfirmation = true;
                }
            }
        },
        saveConfirmed: async function () {
            this.showSaveConfirmation = false;
            this.showChangeConfirmation = false;
            let email = await DATA_CLIENT.checkEmailExists(this.email.email.address);
            if (email) {
                if (await DATA_CLIENT.checkEmailBelongsToOthers(email.id)) {
                    this.enabled = false;
                    this.showBelongsToOthers = true;
                    return;
                }
                this.email.email = email;
                this.showExistingConfirmation = true;
            } else {
                this.$emit('save', this.email);
            }
        },
        saveCancelled: function () {
            this.showSaveConfirmation = false;
            this.showChangeConfirmation = false;
            this.enabled = true;
        },
        existingConfirmed: function () {
            this.$emit('save', this.email);
        },
        existingCancelled: function () {
            this.email.email.id = 0;
            this.showExistingConfirmation = false;
            this.enabled = true;
        },
        belongsToOthersClicked: function () {
            this.showBelongsToOthers = false;
            this.enabled = true;
        }
    },
    mounted: function () {
        $.validator.unobtrusive.parse($(this.$el));
    }
});