"use strict";

Vue.component("SpecialtiesSelector", {
    template: '#specialties-selector-template',
    data: function () {
        return {
            selectedSpecialty: { specialtyId: '', specialty:{}},
            enabled: true
        }
    },
    props: ['level', 'initialEnabled'],
    methods: {
        save: function () {
            if ($(this.$el).valid()) {
                this.$emit('save', this.selectedSpecialty);
                this.selectedSpecialty = { specialtyId: '', specialty: {} };
            }
        },
        cancel: function () {
            let validator = $(this.$el).validate();
            $(this.$el).find(".field-validation-error span")
                .each(function () { validator.settings.success($(this)); });
            validator.resetForm();
            this.selectedSpecialty = {
                specialtyId: '', specialty: {}}
            this.$emit('cancel');
        }
    },
    watch: {
        enabled: function (val) {
            this.$emit('enable', val);
        },
        initialEnabled: function (val) {
            this.enabled = val;
        }
    },
    mounted: function () {
        $.validator.unobtrusive.parse($(this.$el));
    }
});