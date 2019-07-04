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
                departmentId: '',
                admitted: moment(new Date()).format('YYYY-MM-DD')
            },
            enabled:true
        }
    },
    methods: {
        save: function () {
            this.history.hospital = this.hospitals.find(h => h.id == this.history.hospitalId);
            this.history.department = this.departments.find(d => d.id == this.history.departmentId);

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