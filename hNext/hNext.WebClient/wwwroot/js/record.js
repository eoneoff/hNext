"use strict";

Vue.component('Record', {
    template: '#record-template',
    store,
    props:['level', 'initialRecord', 'recordTemplate', 'patientId'],
    data: function () {
        return {
            record: this.initialRecord || {
                patientId:this.patientId,
                header: this.recordTemplate.header,
                date: new Date(),
                recordTemplate: this.recordTemplate,
                recordFields:[]
            }
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
        }
    },
    mounted: function () {
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