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
    });
}

Vue.component("RecordTemplateEditor", {
    template: '#record-template-editor-template',
    props: ['level', 'initialEnabled', 'initialTemplate'],
    data: function () {
        return {
            enabled: true,
            specialties: [],
            doctors: [],
            showFieldEditor: false,
            recordTemplate: this.copyTemplate(),
            selectedField: this.field(),
            noFieldsError: false,
            showSaveConfirmation: false,
            showCancelConfirmation: false
        };
    },
    methods: {
        cancel: function () {
            this.showCancelConfirmation = true;
        },
        save: function () {
            this.noFieldsError = this.recordTemplate.recordFieldTemplates.length == 0;
            if ($(this.$el).valid() && !this.noFieldsError  ) {
                this.showSaveConfirmation = true;
            }
        },
        addNewField: function () {
            this.selectedField = this.field();
            this.showFieldEditor = true;
        },
        addField: function (field) {
            this.noFieldsError = false;
            this.recordTemplate.recordFieldTemplates.push(field);
            this.showFieldEditor = false;
            this.selectedField = {};
        },
        copyTemplate: function () {
            const template = {};
            for (const field in this.initialTemplate) {
                template[field] = this.initialTemplate[field];
            }
            return template;
        },
        field: function () {
            return {
                id: '',
                header: '',
                newLine: false,
                recordFieldType: 0,
                defaultValue: '',
                recordFieldTemplateOptions: []
            };
        },
        defaultOptionValue: function (field) {
            return field.recordFieldTemplateOptions.length > 0
                ? field.recordFieldTemplateOptions.find(o => o.orderNo == 0).value
                : field.defaultValue;
        }
    },
    computed: {
        departments: function () {
            return this.$store.state.departments.departments.filter(d => d.hospitalId == this.recordTemplate.hospitalId);
        },
        filteredDoctors: function () {
            return this.doctors.filter(d => (!this.recordTemplate.specialtyId || d.doctorSpecialties.some(ds => ds.specialtyId == this.recordTemplate.specialtyId))
            && (!this.recordTemplate.departmentId || d.doctorPositions.some(dp => dp.departmentId == this.recordTemplate.departmentId)));
        }
    },
    watch: {
        showFieldEditor: function (val) {
            this.enabled = !val;
        },
        showSaveConfirmation: function (val) {
            this.enabled = !val;
        },
        showCancelConfirmation: function (val) {
            this.enabled = !val;
        }
    },
    mounted: function () {
        DATA_CLIENT.getSpecialties().then(result => this.specialties = result);
        DATA_CLIENT.getDoctors().then(result => this.doctors = result);
        $.validator.unobtrusive.parse($(this.$el));
    }
});