"use strict";

if (!store.state['departments']) {
    const departmentsModule = {
        state: {
            departments: []
        },
        mutations: {
            addDepartment(state, department) {
                let index = state.departments.findIndex(d => d.id == department.id);
                if (index == -1) {
                    state.departments.push(department);
                } else {
                    Vue.set(state.departments, index, department);
                }
            },
            setDepartments(state, departments) {
                state.departments = departments;
            }
        }
    };
    store.registerModule('departments', departmentsModule);
    DATA_CLIENT.getDepartments().then(result => {
        store.commit('setDepartments', result);
    })
}

Vue.component("Departments", {
    template: '#departments-template',
    store,
    data: function () {
        return {
            selectedDepartment: { id: 0 },
            editingDepartment: '',
            editing: false,
            quitConfirmation: false,
            saveConfirmation: false
        }
    },
    computed: {
        enabled: {
            get: function () {
                return store.state.enabled;
            },
            set: function (show) {
                store.commit('enable', show);
            }
        },
        departments: {
            get: function () {
                return this.$store.state.departments.departments;
            },
            set: function (departments) {
                this.$store.commit('setDepartments', departments);
            }
        },
        hospitals: function () {
            return this.$store.state.hospitals.hospitals;
        }
    },
    methods: {
        departmentClicked: function (department) {
            if (this.enabled) {
                this.selectedDepartment = department;
            }
        },
        createNewDepartment: function () {
            this.editingDepartment = { id: 0, name: '', hospitalId:'' };
            this.editing = true;
        },
        editDepartment: function () {
            this.editDepartment = $.extend(true, {}, this.selectedDepartment);
            this.editing = true;
        },
        saveClicked: function () {
            $.validator.unobtrusive.parse(this.$el);
            if ($(this.$el).valid()) {
                this.saveConfirmation = true;
            }
        },
        saveDepartment: async function () {
            this.saveConfirmation = false;
            this.editing = false;
            if (this.editingDepartment.id) {
                this.editingDepartment = await DATA_CLIENT.editDepartment(this.editingDepartment);
            } else {
                this.editingDepartment = await DATA_CLIENT.saveDepartment(this.editingDepartment);
            }
            this.$store.commit('addDepartment', this.editingDepartment);
        },
        cancelEdit: function () {
            this.quitConfirmation = false;
            this.editing = false;
        },
        addEmailToDepartment: async function (email) {
            if (this.selectedDepartment.id) {
                email.departmentId = this.selectedDepartment.id;
                return await DATA_CLIENT.addEmailToDepartment(email);
            }
        },
        deleteEmailFromDepartment: async function (email) {
            if (this.selectedDepartment.id) {
                email.departmentId = this.selectedDepartment.id;
                return await DATA_CLIENT.deleteEmailFromDepartment(email);
            }
        },
        addPhoneToDepartment: async function (phone) {
            if (this.selectedDepartment.id) {
                phone.departmentId = this.selectedDepartment.id;
                return await DATA_CLIENT.addPhoneToDeparment(phone);
            }
        },
        deletePhoneFromDepartment: async function (phone) {
            if (this.selectedDepartment.id) {
                phone.departmentId = this.selectedDepartment.id;
                return await DATA_CLIENT.deletePhoneFromDepartment(phone);
            }
        }
    },
    watch: {
        saveConfirmation: function (val) {
            if (val) {
                this.enabled = false;
            } else {
                this.enabled = true;
            }
        },
        quitConfirmation: function (val) {
            if (val) {
                this.enabled = false;
            } else {
                this.enabled = true;
            }
        }
    },
    provide: function () {
        return {
            addEmail: this.addEmailToDepartment,
            deleteEmail: this.deleteEmailFromDepartment,
            addPhone: this.addPhoneToDepartment,
            deletePhone: this.deletePhoneFromDepartment
        }
    }
});