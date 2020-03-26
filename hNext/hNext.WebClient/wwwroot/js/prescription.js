Vue.component("Prescription", {
    template:'#prescription-template',
    data: function() {
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