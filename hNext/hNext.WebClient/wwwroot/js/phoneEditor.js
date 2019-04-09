"use strict";

Vue.component("PhoneEditor", {
    template: '#phone-editor-template',
    props: ['initialPhone', 'level'],
    data: function () {
        return {
            phone: $.extend(true, {}, this.initialPhone),
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
                if (this.phone.phoneId) {
                    this.showChangeConfirmation = true;
                } else {
                    this.showSaveConfirmation = true;
                }
            }
        },
        saveConfirmed: async function () {
            this.showSaveConfirmation = false;
            this.showChangeConfirmation = false;
            let phone = await DATA_CLIENT.checkPhoneExists(this.phone.phone.number);
            if (phone) {
                if (DATA_CLIENT.checkPhoneBelongsToOthers(phone.phoneId)) {
                    this.enabled = false;
                    this.showBelongsToOthers = true;
                }
                this.phone.phone = phone;
                this.showExistingConfirmation = true;
            } else {
                this.$emit('save', this.phone);
            }
        },
        saveCancelled: function () {
            this.showSaveConfirmation = false;
            this.showChangeConfirmation = false;
            this.enabled = true;
        },
        existingConfirmed: function () {
            this.$emit('save', this.phone);
        },
        existingCancelled: function () {
            this.phone.phone.id = 0;
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