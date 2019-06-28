Vue.component("DiplomaList", {
    template: '#diploma-list-template',
    props:['level', 'initialEnabled', 'initialDiplomas', 'objectSelected'],
    data: function () {
        return {
            diplomas: this.initialDiplomas || [],
            selectedDiploma: {id:0},
            enabled: this.initialEnabled,
            showEditor: false,
            showDeleteConfirmation: false
        }
    },
    methods: {
        selectDiploma: function (diploma) {
            if (this.enabled) {
                this.selectedDiploma = diploma;
            }
        },
        add: function () {
            if (this.objectSelected) {
                this.selectedDiploma = { id: 0 };
                this.showEditor = true;
            }
        },
        edit: function () {
            if (this.selectedDiploma.id) {
                this.showEditor = true;
            }
        },
        remove: function () {
            if (this.selectDiploma.id) {
                this.showDeleteConfirmation = true;
            }
        },
        save: async function (diploma) {
            this.showEditor = false;
            let index = this.diplomas.findIndex(d => d.id == diploma.id);
            if (index == -1) {
                diploma.doctorId = this.objectSelected;
                this.diplomas.push(await DATA_CLIENT.addDiplomaToDoctor(diploma));
            } else {
                this.$set(this.diplomas, index, await DATA_CLIENT.editDiplomaOfDoctor(diploma));
            }
            this.selectedDiploma = { id: 0 };
        },
        deleteDiploma: async function () {
            this.showDeleteConfirmation = false;
            let index = this.diplomas.findIndex(d => d.id == this.selectDiploma.id);
            DATA_CLIENT.removeDiplomaFromDoctor(this.selectedDiploma);
            this.selectedDiploma = { id: 0 };
            this.$delete(this.diplomas, index);
        }
    },
    watch: {
        enabled: function (val) {
            this.$emit('enable', val);
        },
        showEditor: function (val) {
            this.enabled = !val;
        },
        showDeleteConfirmation: function (val) {
            this.enabled = !val;
        },
        initialEnabled: function (val) {
            this.enabled = val;
        },
        initialDiplomas: function (val) {
            this.diplomas = val;
        }
    }
});