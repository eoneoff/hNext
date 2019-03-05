"use strict";

function personEditorModel(id) {
    return new Vue({
        el: `#${id}`,
        data: function () {
            return {
                
            }
        },
        computed: {
            display: {
                get: function () {
                    return store.state.modals[this.$el.id];
                },
                set: function(display) {
                    store.commit('setModalState', {key:this.$el.id, value:display});
                }
            }
        }
    });
}