"use strict";

Vue.component("DoctorPositionList", {
    template: '#doctor-position-list-template',
    props: ['level', 'initialEnabled', 'initialPositions', 'objectSelected'],
    data: function () {
        return {
            positions: this.initialPositions || [],
            selectedPosition: {id:0},
            enabled: this.initialEnabled,
            showPositionEditor: false,
            showDeleteConfirmation:false
        }
    },
    methods: {
        selectPosition: function (position) {
            if (this.enabled) {
                this.selectedPosition = position;
            }
        },
        add: function () {
            if (this.objectSelected) {
                this.selectedPosition = { id: 0 };
                this.showPositionEditor = true;
            }
        },
        edit: function () {
            if (this.selectedPosition.id) {
                this.showPositionEditor = true;
            }
        },
        remove: function () {
            if (this.selectedPosition.id) {
                this.showDeleteConfirmation = true;
            }
        },
        save: async function (position) {
            this.showPositionEditor = false;
            let index = this.positions.findIndex(p => p.id == position.id);
            if (index != -1) {
                this.selectedPosition = { id: 0 };
                this.$set(this.positions, index, await DATA_CLIENT.editPositionOfDoctor(position));
            } else {
                this.selectedPosition = { id: 0 };
                position.doctorId = this.objectSelected;
                position.id = 0;
                this.positions.push(await DATA_CLIENT.addPositionToDoctor(position));
            }
        },
        deletePosition: function () {
            this.showDeleteConfirmation = false;
            let index = this.positions.findIndex(p => p.id == this.selectedPosition.id);
            DATA_CLIENT.deletePositionFromDoctor(this.selectedPosition);
            this.selectedPosition = { id: 0 };
            this.$delete(this.positions, index);
        }
    },
    watch: {
        enabled: function (val) {
            this.$emit('enable', val);
        },
        initialEnabled: function(val) {
            this.enabled = val;
        },
        initialPositions: function (val) {
            this.positions = val || [];
            this.selectedPosition = { id: 0 };
        },
        showPositionEditor: function (val) {
            this.enabled = !val;
        },
        showDeleteConfirmation: function (val) {
            this.enabled = !val;
        }
    }
});