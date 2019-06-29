"use strict";

Vue.component('DocumentsList', {
    template: '#documents-list-template',
    props: ['initialDocuments', 'level', 'personId', 'initialEnabled'],
    data: function () {
        return {
            documents: this.initialDocuments || [],
            selectedDocument: {
                documentId: 0,
                personId: this.personId,
                documentTypeId: 0,
                number: '',
                issuedBy: '',
                dateOfIssue: moment(new Date()).format('YYYY-MM-DD')
            },
            enabled: this.initialEnabled,
            showEditor: false,
            showDeleteConfirmation: false
        }
    },
    methods: {
        selectDocument: function (document) {
            if (this.enabled) {
                this.selectedDocument = document;
            }
        },
        add: function () {
            if (this.personId) {
                this.selectedDocument = {
                    documentId: 0,
                    personId: this.personId,
                    documentTypeId: 0,
                    number: '',
                    issuedBy: '',
                    dateOfIssue: moment(new Date()).format('YYYY-MM-DD')
                } 
                this.enabled = false;
                this.showEditor = true;
            }
        },
        confirmAdd: async function (document) {
            if (document.id) {
                document = await DATA_CLIENT.editDocument(document);
                this.documents.splice(this.documents.findIndex(d => d.id == document.id), 1, document);
                this.selectedDocument = {
                    documentId: 0,
                    personId: this.personId,
                    documentTypeId: 0,
                    number: '',
                    issuedBy: '',
                    dateOfIssue: moment(new Date()).format('YYYY-MM-DD')
                }
            } else {
                document = await DATA_CLIENT.addDocument(document);
                this.documents.push(document);
            }
            this.showEditor = false;
            this.enabled = true;
        },
        edit: function () {
            if (this.selectedDocument.id) {
                this.enabled = false;
                this.showEditor = true;
            }
        },
        cancelEditor: function () {
            this.showEditor = false;
            this.enabled = true;
        },
        remove: function () {
            if (this.selectedDocument.id) {
                this.enabled = false;
                this.showDeleteConfirmation = true;
            }
        },
        confirmRemove: async function () {
            await DATA_CLIENT.deleteDocument(this.selectedDocument.id);
            this.documents.splice(this.documents.indexOf(this.selectedDocument), 1);
            this.selectedDocument = {
                documentId: 0,
                personId: this.personId,
                documentTypeId: 0,
                number: '',
                issuedBy: '',
                dateOfIssue: moment(new Date()).format('YYYY-MM-DD')
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
        initialDocuments: function (val) {
            this.documents = val;
            this.selectedEmail = {
                documentId: 0,
                personId: this.personId,
                documentTypeId: 0,
                number: '',
                issuedBy: '',
                dateOfIssue: moment(new Date()).format('YYYY-MM-DD')
            }
        }
    }
});