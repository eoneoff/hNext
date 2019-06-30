"use strict";

Vue.component("DoctorSpecialtyEditor", {
    template: '#doctor-specialty-editor-template',
    store,
    props:['level', 'initialEnabled', 'initialSpecialty'],
    data: function () {
        return {
            specialty:  this.newSpecialty(),
            enabled: this.initialEnabled,
            showSaveConfirmation: false,
            showCancelConfirmation: false
        }
    },
    computed: {
        issuedDate: {
            get: function () {
                return moment(this.specialty.issuedDate).format('YYYY-MM-DD');
            },
            set: function (val) {
                this.specialty.issuedDate = val;
            }
        },
        expires: {
            get: function () {
                return moment(this.specialty.expires).format('YYYY-MM-DD');
            },
            set: function (val) {
                this.specialty.expires = val;
            }
        }
    },
    watch: {
        enabled: function (val) {
            this.$emit('enable', val);
        },
        showSaveConfirmation: function (val) {
            this.enabled = !val;
        },
        showCancelConfirmation: function (val) {
            this.enabled = !val;
        },
        initialSpecialty: function (val) {
            this.specialty = $.extend(true, this.newSpecialty(), val);
        }
    },
    methods: {
        save: function () {
            if ($(this.$el).valid()) {
                this.showSaveConfirmation = true;
            }
        },
        cancel: function () {
            this.showCancelConfirmation = true;
        },
        saveConfirmed: function () {
            this.showSaveConfirmation = false;
            this.$emit('save', this.specialty);
        },
        saveCancelled: function () {
            let validator = $(this.$el).validate();
            $(this.$el).find(".field-validation-error span")
                .each(function () { validator.settings.success($(this)); });
            validator.resetForm();
            this.specialty = this.newSpecialty();
            this.showCancelConfirmation = false;
            this.$emit('cancel');
        },
        newSpecialty: function () {
            return {
                id: 0,
                specialtyId: '',
                category: '',
                number: '',
                sertifiedBy: '',
                issuedDate: moment(new Date()).format('YYYY-MM-DD'),
                expires: moment(new Date()).format('YYYY-MM-DD')
            }
        }
    },
    mounted: function () {
        $.validator.unobtrusive.parse($(this.$el));
    }
});