"use strict";

Vue.component('main-view', {
    template: '#main-view-template',
    data: function () {
        return {
            selectedTab: START_TAB,
            selectedSideTab: START_SIDE_TAB
        }
    }
});