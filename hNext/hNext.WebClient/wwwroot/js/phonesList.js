"use strict";

Vue.component("PhonesList", {
    template: '#phones-list-template',
    props: ['initialPhones', 'level', 'personId'],
    data: function () {
        return {
            phones: this.initialPhones || [],
            selectedPhone: { phoneId: 0, personId: 0, phone: { id: 0, number: '', phoneTypeId: '' }},
            enabled: true,
            active: this.isPersonSelected,
            showEditor: false,
            showDeleteConfirmation: false
        }
    },
    methods: {
        selectPhone: function (phone) {
            if (this.enabled) {
                this.selectedPhone = phone;
            }
        },
        add: function () {
            if (this.personId) {
                this.selectedPhone = { phoneId: 0, personId: this.personId, phone: { id: 0, number: '', phoneTypeId: '' } };
                this.enabled = false;
                this.showEditor = true;
            }
        },
        confirmAdd: async function (phone) {
            this.showEditor = false;
            this.enabled = true;
            if (this.phone.phoneId) {
                phone.phone = await DATA_CLIENT.editPhone(phone.phone);
                this.phones.splice(
                    this.phones.findIndex((p) => p.phoneId == phone.phoneId),
                    1, phone);
            } else {
                phone = await DATA_CLIENT.addPhoneToPerson(phone);
                this.phones.push(phone);
            }
            
        },
        edit: function () {
            if (this.selectedPhone.phoneId) {
                this.enabled = false;
                this.showEditor = true;
            }
        },
        cancelEditor: function () {
            this.showEditor = false;
            this.enabled = true;
        },
        remove: function () {
            if (this.selectedPhone.id) {
                this.enabled = false;
                this.showDeleteConfirmation = true;
            }
        },
        confirmRemove: async function () {
            this.phones.splice(this.phones.indexOf(this.selectedPhone),1);
            this.selectedPhone = { id: 0, name: '', phoneTypeId: '' };
            this.showDeleteConfirmation = false;
            this.enabled = true;
            await DATA_CLIENT.deletePhoneFromPerson(this.selectedPhone.personId, this.selectedPhone.phoneId);
        },
        cancelRemove: function () {
            this.enabled = true;
            this.showDeleteConfirmation = false;
        }
    }
});