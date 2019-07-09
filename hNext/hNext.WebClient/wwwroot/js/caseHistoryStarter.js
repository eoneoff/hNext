"use strict";

Vue.component("CaseHistoryStarter",{
    template: '#case-history-starter-template',
    props: ['level'],
    store,
    data: function () {
        return {
            history: {
                documentRegistry: {},
                hospitalId: '',
                admitted: moment(new Date()).format('YYYY-MM-DD')
            },
            departmentId:'',
            enabled:true
        }
    },
    methods: {
        save: function () {
            if (this.departmentId) {
                this.history.admissions = [{ departmentId: this.departmentId }];
            }

            this.$emit('save', this.history);
        },
        cancel: function () {
            this.$emit('cancel');
        }
    },
    computed: {
        hospitals: function () {
            return this.$store.state.hospitals.hospitals;
        },
        departments: function () {
            return this.$store.state.departments.departments.filter(d => d.hospitalId == this.history.hospitalId);
        }
    }
});