"use strict";

Vue.component('IcdReference', {
    template: '#icd-reference-template',
    props: ['level', 'initialEnabled'],
    data: function () {
        return {
            enabled: true,
            searchQuery:'',
            selectedICD: { id: 0 }
        }
    },
    store,
    methods: {
        save: function () {
            this.$emit('save', selectedICD);
        },
        cancel: function () {
            this.$emit('cancel');
        },
        htmlify: function (name) {
            return name.replace(/\W+/gi, '');
        },
        highlightSelected: function (name) {
            return name.replace(new RegExp(this.searchQuery,'gi'), '<mark>$&</mark>')
        }
    },
    watch: {
        searchQuery: function (val, prev) {
            if ((val.length >= 3 && prev.length < 3) || (val.length < 3 && prev.length >= 3)
                || (val.length >= 3 && val.length > prev.length && this.selectedICD.id &&
                    this.foundItems.findIndex(i => i.id == this.selectedICD.id) == -1)) {
                this.selectedICD = { id: 0 };
            }
        }
    },
    computed: {
        icd: function () {
            return this.$store.state.icd.icd;
        },
        foundItems: function () {
            if (this.searchQuery.length >= 3) {
                return this.$store.state.icd.icdRaw.filter(i => i.name.toLowerCase()
                    .indexOf(this.searchQuery.toLowerCase()) != -1);
            }
        }
    }
});