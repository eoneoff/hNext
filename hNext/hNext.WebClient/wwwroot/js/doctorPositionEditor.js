"use strict";

Vue.component("DoctorPositionEditor", {
    template: '#doctor-position-editor-template',
    props: ['level', 'initialEnabled', 'initialPosition'],
    inject: ['specialties'],
    store,
    data: function () {
        return {
            enabled: this.initialEnabled,
            position: this.initialPosition.id ? this.initialPosition : this.newPosition(),
            specialtiesList: [],
            showSaveConfirmation: false,
            showCancelConfirmation:false
        }
    },
    computed: {
        hospitals: function () {
            return this.$store.state.hospitals.hospitals;
        },
        departments: function () {
            return this.$store.state.departments.departments.filter(d => d.hospitalId == this.position.hospitalId);
        },
        startDate: {
            get: function () {
                return moment(this.position.startDate).format('YYYY-MM-DD');
            },
            set: function (val) {
                this.position.startDate = val;
            }
        },
        endDate: {
            get: function () {
                return moment(this.position.endDate).format('YYYY-MM-DD');
            },
            set: function (val) {
                this.position.endDate = val;
            }
        }
    },
    methods: {
        save: function () {
            if ($(this.$el).valid()) {
                this.showSaveConfirmation = true;
            }
        },
        newPosition: function () {
            return {
                id: '',
                specialtyId: '',
                positionId: '',
                hospitalId: '',
                departmentId: '',
                startDate: '',
                endDate: ''
            }
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
        this.specialtiesList = this.specialties();
        $.validator.unobtrusive.parse($(this.$el));
    }
});