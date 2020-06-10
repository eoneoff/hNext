"use strict";

Vue.component("CaseHistoryPrescriptions", {
    template: "#case-history-prescriptions-template",
    store,
    data: function() {
        return {
            prescription:{},
            showAddPrescription: false
        }
    },
    computed: {
        prescriptions:function() {
            return (this.$store.state.caseHistory.history.prescriptions || []).sort((p1, p2) => p1.prescription.startDate - p1.prescription.startDate);
        },
        enabled: {
            get: function () {
                return this.$store.state.enabled;
            },
            set: function (val) {
                this.$store.commit('enable', val);
            }
        },
        caseHistoryId: function () {
            return this.$store.state.caseHistory.history.id;
        } 
    },
    watch: {
        showAddPrescription: function (val) {
            this.enabled = !val;
        }
    },
    methods: {
        addPrescriptionClicked: function(){
            this.showAddPrescription = true;
        }
    }
});