"use strict";

Vue.component('CaseHistoryDiagnosysEditor', {
    template: '#case-history-diagnosys-editor-template',
    props:['level', 'initialEnabled'],
    data: function () {
        return {
            enabled: true,
            showDiagnosysEditor: false
        }
    },
    methods: {
        save: function () {

        },
        cancel: function () {
            this.$emit('cancel');
        },
        addDiagnosys: async function () {

        }
    },
    watch: {
        showDiagnosysEditor: function (val) {
            this.enabled = !val;
        }
    },
    mounted: function() {
        $.validator.unobtrusive.parse($(this.$el));
    }
});