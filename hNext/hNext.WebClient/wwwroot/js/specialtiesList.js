Vue.component("SpecialtiesList", {
    template: '#specialties-list-template',
    props: ['editButton', 'level', 'initialEnabled', 'initialSpecialties', 'objectSelected'],
    inject: ['addSpecialty', 'deleteSpecialty', 'editSpecialty'],
    data: function () {
        return {
            specialties: this.initialSpecialties || [],
            selectedSpecialty: {specialtyId:0},
            enabled: this.initialEnabled,
            showAddSpecialty: false,
            specialtyExists: false,
            showDeleteSpecialty: false
        }
    },
    methods: {
        selectSpecialty: function (specialty) {
            if (this.enabled) {
                this.selectedSpecialty = specialty;
            }
        },
        add: function () {
            if (this.objectSelected) {
                this.selectedSpecialty = {specialtyId:0}
                this.enabled = false;
                this.showAddSpecialty = true;
            }
        },
        edit: function () {
            if (this.objectSelected) {
                this.enabled = false;
                this.showAddSpecialty = true;
            }
        },
        remove: function () {
            if (this.selectedSpecialty.specialtyId) {
                this.showDeleteSpecialty = true;
            }
        },
        hideSpecialtyAdder() {
            this.showAddSpecialty = false;
            this.enabled = true;
        },
        save: async function (specialty) {
            this.showAddSpecialty = false;
            this.enabled = true;
            let index = this.specialties.findIndex(s => s.specialtyId == specialty.specialtyId);
            if (index == -1) {
                this.specialties.push(await this.addSpecialty(specialty));
            } else {
                Vue.set(this.specialties, index, await this.editSpecialty(specialty));
            }
        },
        confirmRemove: async function () {
            this.showDeleteSpecialty = false;
            let index = this.specialties.findIndex(s => s.specialtyId == this.selectedSpecialty.specialtyId);
            if (index != -1) {
                this.deleteSpecialty(this.selectedSpecialty);
                Vue.delete(this.specialties, index);
            }
        }
    },
    watch: {
        enabled: function (val) {
            this.$emit('enable', val);
        },
        initialEnabled: function (val) {
            this.enabled = val;
        },
        initialSpecialties: function (val) {
            this.specialties = val || [];
            this.selectedSpecialty = { specialtyId: 0 };
        },
        specialtyExists: function (val) {
            this.enabled = !val;
        },
        showDeleteSpecialty: function (val) {
            this.enabled = !val;
        }
    }
});