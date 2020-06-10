"use strict";

Vue.component("DrugPrescription", {
    template: "#drug-prescription-template",
    data: function () {
        return {

        }
    }, 
    computed: {
        enabled: {
            get: function () {
                return this.$store.state.enabled;
            },
            set: function (val) {
                this.$store.commit('enable', val);
            }
        }
    }
});