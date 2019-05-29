"use strict";

Vue.component('main-view', {
    template: '#main-view-template',
    data: function () {
        return {
            selectedTab: START_TAB,
            selectedSideTab: START_SIDE_TAB
        }
    },
    store,
    computed: {
        enabled: {
            get: function () {
                return store.state.enabled;
            },
            set: function (val) {
                store.commit('enable', show);
            }
        }
    },
    methods: {
        tabClicked: function (tabName) {
            if (this.enabled) {
                this.selectedTab = tabName;
            }
        },
        sideTabClicked: function (tabName) {
            if (this.enabled) {
                this.selectedSideTab = tabName;
            }
        }
    }
});