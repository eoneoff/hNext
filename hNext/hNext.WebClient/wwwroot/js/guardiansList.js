"use strict";

Vue.component('GuardiansList', {
    template: '#guardians-list-template',
    props: ['initialGuardians', 'level', 'personId', 'initialEnabled'],
    data: function () {
        return {
            guardians: this.initialGuardians || [],
            selectedGuardian: {
                guardianId: 0,
                wardId: this.personId,
                relation: '',
                guardian: {
                    id: 0,
                    firstName: '',
                    familyName: '',
                    patronimic:''
                }
            },
            enabled: this.initialEnabled,
            showEditor: false,
            showDeleteConfirmation: false
        }
    },
    methods: {
        selectGuardian: function (guardian) {
            if (this.enabled) {
                this.selectedGuardian = guardian;
            }
        },
        add: function () {
            if (this.personId) {
                this.selectedGuardian = {
                    guardianId: 0,
                    wardId: this.personId,
                    relation: '',
                    guardian: {
                        id: 0,
                        firstName: '',
                        familyName: '',
                        patronimic: ''
                    }
                };
                this.enabled = false;
                this.showEditor = true;
            }
        },
        edit: function () {
            if (this.selectedGuardian.guardianId) {
                this.enabled = false;
                this.showEditor = true;
            }
        },
        confirmAdd: async function (guardian) {
            let existing = await DATA_CLIENT.guardianRelationExists(guardian);
            if (existing) {
                guardian = await DATA_CLIENT.editGuardian(guardian);
                this.selectedGuardian = guardian;
                this.guardians.splice(this.guardians.indexOf(this.selectedGuardian), 1, guardian);
            } else {
                guardian = await DATA_CLIENT.addGuardian(guardian);
                this.guardians.push(guardian);
            }

            this.showEditor = false;
            this.enabled = true;
        },
        cancelEditor: function () {
            this.showEditor = false;
            this.enabled = true;
        },
        remove: function () {
            if (this.selectedGuardian.guardianId) {
                this.enabled = false;
                this.showDeleteConfirmation = true;
            }
        },
        confirmRemove: async function () {
            DATA_CLIENT.deleteGuardian(this.selectedGuardian);
            this.guardians.splice(this.guardians.indexOf(this.selectedGuardian), 1);
            this.selectedGuardian = {
                guardianId: 0,
                wardId: this.personId,
                relation: '',
                guardian: {
                    id: 0,
                    firstName: '',
                    familyName: '',
                    patronimic: ''
                }
            };
            this.showDeleteConfirmation = false;
            this.enabled = true;
        },
        cancelRemove: function () {
            this.showDeleteConfirmation = false;
            this.enabled = true;
        }
    },
    watch: {
        enabled: function (val) {
            this.$emit('enable', val);
        },
        initialEnabled: function (val) {
            this.enabled = val;
        },
        initialGuardians: function (val) {
            this.guardians = val;
            this.selectedGuardian = '';
        }
    }
});