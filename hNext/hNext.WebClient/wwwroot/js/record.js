"use strict";

Vue.component('Record', {
    template: '#record-template',
    store,
    props:['level', 'initialRecord', 'recordTemplate', 'patientId', 'editor', 'initialDiagnoses', 'initialPrecriptions'],
    inject:{baseSaveDiagnosys:'saveDiagnosys'},
    data: function () {
        return {
            record: this.initialRecord || {
                patientId:this.patientId,
                header: this.recordTemplate.header,
                date: new Date(),
                recordTemplate: this.recordTemplate,
                recordFields: [],
                diagnoses:[]
            },
            editMode: false,
            emptyDateError: false,
            showEmptyFieldsWarning: false,
            showSaveConfirmation: false,
            showCancelConfirmation: false,
            showDeleteConfirmation: false,
            showDiagnosysEditor: false
        };
    },
    computed: {
        enabled: {
            get: function () {
                return this.$store.state.enabled;
            },
            set: function (val) {
                this.$store.commit('enable', val);
            }
        },
        startDate: function () {
            return moment(this.$store.state.caseHistory.history.admitted).isValid()
                ? moment(this.$store.state.caseHistory.history.admitted).format('YYYY-MM-DD')
                : '';
        },
        date: {
            get: function () {
                return moment(this.record.date).isValid()
                    ? moment(this.record.date).format('YYYY-MM-DD')
                    : '';
            },
            set: function (val) {
                this.emptyDateError = false;
                const date = val ? val.split('-') : [new Date().getFullYear(), new Date().getMonth(), new Date().getDate()];
                this.record.date.setFullYear(date[0]);
                this.record.date.setMonth(+date[1]-1);
                this.record.date.setDate(date[2]);
                this.record.date = new Date(this.record.date);
            }
        },
        time: {
            get: function () {
                return moment(this.record.date).isValid()
                    ? moment(this.record.date).format('HH:mm')
                    : '';
            },
            set: function (val) {
                const time = val ? val.split(':') : [0,0];
                this.record.date.setHours(time[0]);
                this.record.date.setMinutes(time[1]);
                this.record.date = new Date(this.record.date);
            }
        },
        fields: function () {
            return this.record.recordFields.sort((f1, f2) => f1.orderNo - f2.orderNo);
        },
        diagnoses: function () {
            (this.nitialDiagnoses || []).filter(d => this.record.diagnoses.findIndex(rd => rd.diagnosysId == d.diagnosysId) >= 0);
        },
        moment: function () {
            return moment;
        }
    },
    watch: {
        showEmptyFieldsWarning: function (val) {
            this.enabled = !val;
        },
        showSaveConfirmation: function (val) {
            this.enabled = !val;
        },
        showCancelConfirmation: function (val) {
            this.enabled = !val;
        },
        showDeleteConfirmation: function (val) {
            this.enabled = !val;
        },
        showDiagnosysEditor: function (val) {
            this.enabled = !val;
        }
    },
    methods: {
        insertRecordRow: function (row) {
            const field = {
                orderNo: +row + 1,
                value: ''
            };
            for (let i = +row + 1; i < this.record.recordFields.length; i++) {
                this.record.recordFields[i].orderNo++;
            }
            this.record.recordFields.push(field);
            this.record.recordFields.sort((f1, f2) => f1.orderNo - f2.orderNo);
        },
        deleteRecordRow: function (row) {
            this.record.recordFields.splice(row, 1);
            for (let i = +row; i < this.record.recordFields.length; i++) {
                this.record.recordFields[i].orderNo--;
            }
        },
        saveClicked: function () {
            this.emptyDateError = !this.date;
            if ($(this.$refs.editor).valid() && !this.emptyDateError) {
                if (this.record.recordFields.some(f => !f.value)) {
                    this.showEmptyFieldsWarning = true;
                } else {
                    if (this.editor) {
                        this.showSaveConfirmation = true;
                    } else {
                        this.editMode = false;
                        this.$emit('save', this.record);
                    }
                }
            }
        },
        cancelClicked: function () {
            if (this.editor) {
                this.showCancelConfirmation = true;
            } else {
                this.editMode = false;
            }
        },
        saveFiltered: function () {
            this.record.recordFields = this.record.recordFields.filter(f => f.value);
            if (this.editor) {
                this.$emit('save', this.record);
            } else {
                this.editMode = false;
            }
        },
        save: function () {
            this.enabled = true;
            this.$emit('save', this.record);
        },
        exit: function () {
            this.enabled = true;
            this.$emit('cancel');
        },
        deleteRecord: function () {
            this.enabled = true;
            this.showDeleteConfirmation = false;
            this.$emit('delete');
        },
        saveDiagnosis: function(diagnosys) {
            this.showDiagnosysEditor = false;
            this.baseSaveDiagnosys(diagnosys);
        },
        closeDiagnosysEditor: function () {
            this.showDiagnosysEditor = false;
        }
    },
    mounted: function () {
        $.validator.unobtrusive.parse($(this.$refs.editor));
        if (!this.initialRecord) {
            this.record.recordFields = this.recordTemplate.id
                ? this.recordTemplate.recordFieldTemplates.map(ft => {
                    return {
                        recordFieldTemplateId: ft.id,
                        orderNo: ft.orderNo,
                        value: ft.defaultValue,
                        recordFieldTemplate:ft
                    };
                })
                : [{
                    orderNo: 0,
                    value:''
                }];
            this.record.recordFields.sort((f1, f2) => f1.orderNo - f2.orderNo);
        }
    }
});