"use strict";

Vue.component("Prescription", {
    template:'#prescription-template',
    props:['level'],
    data: function() {
        return {
            endDateMode: 0,
            enabled: true,
            prescriptionSchema: 0,
            schema:[]
        }
    },
    computed: {
        schemaLength: {
            get: function() {
                return this.schema.length;
            },
            set: function (value) {
                if (value < this.schema.length) {
                    while (this.schema.length > value) {
                        this.schema.pop();
                    }
                } else {
                    while (this.schema.length < value) {
                        this.schema.push(false);
                    }
                }
            }
        }
    },
    methods: {
        cancel: function () {
            this.$emit('cancel');
        }
    }
});