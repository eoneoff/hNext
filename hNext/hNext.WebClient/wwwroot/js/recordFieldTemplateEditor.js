"use strict";

Vue.component('RecordFieldTemplateEditor', {
    template: '#record-field-template-editor-template',
    props: ['level', 'initialEnabled', 'initialField'],
    data: function () {
        return {
            enabled: true,
            field: this.initialField,
            selectedOption: {},
            newOptionValue: '',
            showSaveConfirmation: false,
            showEditOptionConfirmation: false,
            showDeleteOptionConfirmation: false
        };
    },
    methods: {
        save: function () {
            if ($(this.$refs.formfield).valid()) {
                this.showSaveConfirmation = true;
            }
        },
        cancel: function () {
            this.$emit('cancel');
        },
        addOption: function () {
            if ($(this.$refs.option).valid()) {
                this.field.recordFieldTemplateOptions.push({
                    recordFieldTemplateId: this.field.id,
                    value: this.newOptionValue,
                    orderNo:this.field.recordFieldTemplateOptions.length
                });
                this.selectedOption = {};
                this.newOptionValue = '';
            }
        },
        optionClicked: function (option) {
            this.selectedOption = option;
            this.newOptionValue = option.value;
        },
        editOption: function () {
            if (this.selectedOption) {
                this.showEditOptionConfirmation = true;
            }
        },
        changeOption: function () {
            this.selectedOption.value = this.newOptionValue;
            this.showEditOptionConfirmation = false;
        },
        moveOptionUp: function () {
            if (this.selectedOption && this.selectedOption.orderNo > 0) {
                let upperOption = this.field.recordFieldTemplateOptions.find(o => o.orderNo == this.selectedOption.orderNo - 1);
                upperOption.orderNo = this.selectedOption.orderNo;
                this.selectedOption.orderNo--;
                this.field.recordFieldTemplateOptions.sort((o1, o2) => o1.orderNo - o2.orderNo);
            }
        },
        moveOptionDown: function () {
            if (this.selectedOption && this.selectedOption.orderNo < this.field.recordFieldTemplateOptions.length - 1) {
                let lowerOption = this.field.recordFieldTemplateOptions.find(o => o.orderNo == this.selectedOption.orderNo + 1);
                lowerOption.orderNo = this.selectedOption.orderNo;
                this.selectedOption.orderNo++;
                this.field.recordFieldTemplateOptions.sort((o1, o2) => o1.orderNo - o2.orderNo);
            }
        },
        deleteOption: function () {
            if (this.selectedOption) {
                this.showDeleteOptionConfirmation = true;
            }
        },
        removeOption: function () {
            const index = this.field.recordFieldTemplateOptions.indexOf(this.selectedOption);
            this.$delete(this.field.recordFieldTemplateOptions, index);
            for (let i = index; i < this.field.recordFieldTemplateOptions.length; i++) {
                this.field.recordFieldTemplateOptions[i].orderNo--;
            }
            this.showDeleteOptionConfirmation = false;
            this.selectedOption = {};
        }
    },
    watch: {
        'field.recordFieldType': function () {
            this.field.defaultValue = '';
            this.field.recordFieldTemplateOptions = [];
        },
        showSaveConfirmation(val) {
            this.enabled = !val;
        },
        showEditOptionConfirmation: function (val) {
            this.enabled = !val;
        },
        showDeleteOptionConfirmation: function (val) {
            this.enabled = !val;
        }
    },
    mounted: function() {
        $.validator.unobtrusive.parse($(this.$refs.formfield));
    },
    updated: function () {
        if (this.field.recordFieldType == 3) {
            $.validator.unobtrusive.parse($(this.$refs.option));
        }
    }
});