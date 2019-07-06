"use strict";

Vue.component("CaseHistoriesList", {
    template: '#case-histories-list-template',
    store,
    data: function () {
        return {
            showEditor:false
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
        },
        histories: function () {
            return this.patientId ?
                this.$store.state.patient.patient.caseHistories : []
        },
        patientId: function () {
            return this.$store.state.patient.patient.id;
        }
    },
    methods: {
        add: function () {
            if (this.patientId) {
                this.showEditor = true;
            }
        },
        save: async function (history) {
            this.showEditor = false;
            history.patientId = this.patientId;
            history.documentRegistry.creationDate = new Date();
            this.histories.push(await DATA_CLIENT.addCaseHistory(history));
        },
        date: function (history) {
            return this.$options.filters.shortDate(history.admitted)
                + (history.discharged ? ` - ${this.$options.filters.shortDate(history.discharged)}` : '');
        },
        openHistory: function(id) {
            window.open(`casehistories/${id}`, "_blank");
        }
    },
    watch: {
        showEditor: function (val) {
            this.enabled = !val;
        }
    }
});