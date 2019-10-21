"use strict";

Vue.component('RecordFieldTemplateEditor', {
    template: '#record-field-template-editor-template',
    props: ['level', 'initialEnabled'],
    data: function () {
        return {
            enabled: true,
            field: {
                header: '',
                newLine: false,
                recordFieldType: 0,
                defaultValue:''
            }
        };
    },
    methods: {
        save: function () {
            if ($(this.$el).valid()) {

            }
        },
        cancel: function () {
            this.$emit('cancel');
        }
    },
    watch: {
        'field.recordFieldType': function () {
            this.field.defaultValue = '';
        }
    },
    mounted: function() {
        $.validator.unobtrusive.parse($(this.$el));
    }
});