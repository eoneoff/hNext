"use strict";

Vue.component('DiplomaEditor', {
    template: '#diploma-editor-template',
    props:['level','initialEnabled' ,'initialDiploma'],
    data: function () {
        return {
            diploma: this.initialDiploma,
            enabled: this.initialEnabled,
            showSaveConfirmation: false,
            showCancelConfirmation:false
        }
    },
    methods: {
        save: function () {
            if ($(this.$el).valid()) {
                this.showSaveConfirmation = true;
            }
        },
        cancel: function () {
            this.$emit('cancel');
        }
    },
    watch: {
        showSaveConfirmation: function (val) {
            this.enabled = !val;
        },
        showCancelConfirmation: function (val) {
            this.enabled = !val;
        }
    },
    mounted: function() {
        $.validator.unobtrusive.parse($(this.$el));
    }
});