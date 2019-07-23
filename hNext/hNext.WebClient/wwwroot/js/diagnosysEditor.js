"use strict";

Vue.component("DiagnosysEditor", {
    template: '#diagnosys-template',
    props: ['level', 'initialEnabled'],
    data: function () {
        return {
            enabled:true
        }
    },
    methods: {
        save: function () {

        },
        cancel: function () {
            this.$emit('cancel');
        }
    },
    mounted: function () {
        $.validator.unobtrusive.parse($(this.$el));
    }
});