"use strict";

function personEditorModel(id, level = 1) {
    return new Vue({
        el: `#${id}`,
        data: function () {
            return {
                modalLevel: level
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
            },
            enabled: function() {
                return store.state.openModals = this.modalLevel;
            }
        },
        methods: {
            close: function () {
                store.commit('closeModal');
                this.display = false;
            }
        }
    });
}