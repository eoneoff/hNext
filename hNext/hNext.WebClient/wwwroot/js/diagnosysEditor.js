"use strict";

if (!store.state['icd']) {
    const ICDModule = {
        state: {
            icd: [],
            icdRaw:[]
        },
        mutations: {
            setICD: function (state, icd) {
                state.icdRaw = icd;
                state.icd = icd.reduce((categories, item) => {
                    if (!categories[item.category]) {
                        categories[item.category] = {};
                    }
                    if (!categories[item.category][item.subCategory]) {
                        categories[item.category][item.subCategory] = {};
                    };
                    if (!categories[item.category][item.subCategory][item.primaryName]) {
                        categories[item.category][item.subCategory][item.primaryName] = [];
                    }
                    categories[item.category][item.subCategory][item.primaryName].push(item);
                    return categories;
                }, {});
            }
        }
    };
    store.registerModule("icd", ICDModule);
    DATA_CLIENT.getICD().then(result => store.commit('setICD', result));
};

Vue.component("DiagnosysEditor", {
    template: '#diagnosys-editor-template',
    props: ['level', 'initialEnabled'],
    data: function () {
        return {
            enabled: true,
            showReference: false
        }
    },
    methods: {
        save: function () {

        },
        cancel: function () {
            this.$emit('cancel');
        },
        reference: function () {
            this.showReference = true;
        }
    },
    watch: {
        showReference: function (val) {
            this.enabled = !val;
        }
    },
    mounted: function () {
        $.validator.unobtrusive.parse($(this.$el));
    }
});