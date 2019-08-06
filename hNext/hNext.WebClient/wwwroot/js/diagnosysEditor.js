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
                    }
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
}

Vue.component("DiagnosysEditor", {
    template: '#diagnosys-editor-template',
    props: ['level', 'initialEnabled'],
    data: function () {
        return {
            enabled: true,
            showReference: false,
            showSaveConfirmation: false,
            showCancelConfirmation: false,
            showIcdConfirmation: false,
            icdConfirmationText:'',
            editMode: false,
            diagnosys: {},
            icd: {}
        };
    },
    methods: {
        save: function () {
            if ($(this.$el).valid()) {
                if (this.icd.letter && this.icd.primaryNumber && this.icd.secondaryNumber) {
                    let icd = this.$store.state.icd.icdRaw.find(i =>
                        i.letter == this.icd.letter &&
                        i.primaryNumber == this.icd.primaryNumber &&
                        i.secondaryNumber == this.icd.secondaryNumber);
                    if (icd) {
                        this.diagnosys.icdId = id;
                        this.diagnosys.icd = icd;
                        this.showSaveConfirmation = true;
                    }
                    else {
                        this.icdConfirmationText = WRONG_ICD;
                        this.showIcdConfirmation = true;
                    }
                } else if (this.diagnosys.icdId) {
                    this.showSaveConfirmation = true;
                } else {
                    this.icdConfirmationText = EMPTY_ICD;
                    this.showIcdConfirmation = true;
                }
            }
        },
        cancel: function () {
            this.$emit('cancel');
        },
        reference: function () {
            this.showReference = true;
        },
        getICDCode: function (icd) {
            this.$set(this.diagnosys, 'icd', icd);
            this.$set(this.diagnosys, 'icdId', icd.id);
            this.showReference = false;
        }
    },
    computed: {
        ICDCode: function () {
            let icd = "";
            if (this.diagnosys.icd) {
                icd = this.diagnosys.icd.letter;
                icd = icd + (this.diagnosys.icd.primaryNumber < 10 ? `0${this.diagnosys.icd.primaryNumber}` : this.diagnosys.icd.primaryNumber);
                icd = icd + `.${this.diagnosys.icd.secondaryNumber}`;
            }
            return icd;
        }
    },
    watch: {
        showReference: function (val) {
            this.enabled = !val;
        },
        editMode: function (val) {
            if (val && this.diagnosys.icd) {
                this.diagnosys.icdId = 0;
                this.diagnosys.icd = {};
            }
        },
        showSaveConfirmation: function (val) {
            this.enabled = !val;
        },
        showCancelConfirmation: function (val) {
            this.enabled = !val;
        },
        showIcdConfirmation: function (val) {
            this.enabled = !val;
        }
    },
    mounted: function () {
        $.validator.unobtrusive.parse($(this.$el));
    }
});