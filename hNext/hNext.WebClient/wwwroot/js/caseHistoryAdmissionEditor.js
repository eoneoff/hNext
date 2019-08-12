"use strict";

Vue.component("CaseHistoryAdmissionEditor", {
    template: '#case-history-admission-editor-template',
    props: ['level', 'initialEnabled'],
    store,
    data: function () {
        return {
            enabled: true,
            showSaveConfirmation: false,
            showCancelConfirmation:false,
            admission: { departmentId:''}
        };
    },
    computed: {
        departments: function () {
            return this.$store.state.caseHistory.history.hospital.departments || [];
        }
    },
    methods: {
        save: function () {
            this.showSaveConfirmation = true;
        },
        cancel: function () {
            this.showCancelConfirmation = true;
        }
    }
});