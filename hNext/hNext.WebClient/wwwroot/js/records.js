"use strict";

Vue.component("Records", {
    template: '#records-template',
    store,
    data: function () {
        return {
            showTemplateEditor:false
        };
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
    },
    watch: {
        showTemplateEditor: function (val) {
            this.enabled = !val;
        }
    }
});