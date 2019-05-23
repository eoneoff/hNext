"use strict";

Vue.component("PhonesList", {
    template: '#phones-list-template',
    props: ['initialPhones', 'level', 'initialEnabled', 'objectSelected'],
    inject: ['addPhone', 'deletePhone'],
    data: function () {
        return {
            phones: this.initialPhones || [],
            selectedPhone: { phoneId: 0, phone: { id: 0, number: '', phoneTypeId: '' }},
            enabled: this.initialEnabled,
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
            if (this.objectSelected) {
                this.selectedPhone = { phoneId: 0, phone: { id: 0, number: '', phoneTypeId: '' } };
                this.enabled = false;
                this.showEditor = true;
            }
        },
        confirmAdd: async function (phone) {
            this.showEditor = false;
            this.enabled = true;
            if (phone.phoneId) {
                phone.phone = await DATA_CLIENT.editPhone(phone.phone);
                this.phones.splice(
                    this.phones.findIndex((p) => p.phoneId == phone.phoneId),
                    1, phone);
            } else {
                phone = await this.addPhone(phone);
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
            this.selectedPhone = { phoneId: 0, phone: { id: 0, number: '', phoneTypeId: '' } },
            this.showDeleteConfirmation = false;
            this.enabled = true;
            await this.deletePhone(this.selectedPhone);
        },
        cancelRemove: function () {
            this.enabled = true;
            this.showDeleteConfirmation = false;
        }
    },
    watch: {
        enabled: function (val) {
            this.$emit('enable', val);
        },
        initialEnabled: function (val) {
            this.enabled = val;
        },
        initialPhones: function () {
            this.phones = this.initialPhones;
            this.selectedPhone = { phoneId: 0, phone: { id: 0, number: '', phoneTypeId: '' } };
        }
    }
});