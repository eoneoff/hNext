"use strict";

Vue.component('EmailsList', {
    template: '#emails-list-template',
    props: ['initialEmails', 'level', 'personId', 'initialEnabled'],
    data: function () {
        return {
            emails: this.initialEmails || [],
            selectedEmail: { emailId: 0, personId: this.personId, email: { id: 0, address: '' } },
            enabled: this.initialEnabled,
            showEditor: false,
            showDeleteConfirmation: false
        }
    },
    methods: {
        selectEmail: function (email) {
            if (this.enabled) {
                this.selectedEmail = email;
            }
        },
        add: function () {
            if (this.personId) {
                this.selectedEmail = { emailId: 0, personId: this.personId, email: { id: 0, address: '' } }
                this.enabled = false;
                this.showEditor = true;
            }
        },
        confirmAdd: async function (email) {
            this.showEditor = false;
            this.enabled = true;
            if (email.emailId) {
                email.email = await DATA_CLIENT.editEmail(email.email);
                this.emails.splice(
                    this.emails.findIndex((e) => e.emailId == email.emailId),
                    1, email);
            } else {
                email = await DATA_CLIENT.addEmailToPerson(email);
                this.emails.push(email);
            }
        },
        edit: function () {
            if (this.selectedEmail.emailId) {
                this.enabled = false;
                this.showEditor = true;
            }
        },
        cancelEditor: function () {
            this.showEditor = false;
            this.enabled = true;
        },
        remove: function () {
            if (this.selectedEmail.emailId) {
                this.enabled = false;
                this.showDeleteConfirmation = true;
            }
        },
        confirmRemove: async function () {
            this.emails.splice(this.emails.indexOf(this.selectedEmail), 1);
            this.showDeleteConfirmation = false;
            this.enabled = true;
            await DATA_CLIENT.deleteEmailFromPerson(this.selectedEmail);
            this.selectedEmail = { emailId: 0, personId: 0, email: { id: 0, address: '' } }
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
        initialEmails: function (val) {
            this.emails = val;
            this.selectedEmail = { emailId: 0, personId: 0, email: { id: 0, address: '' } };
        }
    }
});