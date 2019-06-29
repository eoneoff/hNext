"use strict";

Vue.component("Doctor", {
    template: "#doctor-template",
    data: function() {
        return {
            doctors: [],
            searchModel: {
                name: '',
                specialtyId: '',
                hospitalId:''
            },
            selectedDoctor: {
                id: 0, personId: 0, person: {} },
            editingDoctor: {},
            editing: false,
            searching: false,
            showPersonEditor: false,
            showSaveConfirmation: false,
            showCancelConfirmation: false
        }
    },
    store,
    computed: {
        enabled: {
            get: function () {
                return this.$store.state.enabled;
            },
            set: function (enable) {
                this.$store.commit('enable', enable);
            }
        },
        hospitals: function() {
            return this.$store.state.hospitals.hospitals;
        },
        currentSpecialties: function () {
            if (this.selectedDoctor.id) {
                return this.selectedDoctor.doctorSpecialties.filter((val, index, arr) => arr.findIndex(s => s.id == val.id) == index)
                    .map(s => s.specialty);
            }
            return [];
        }
    },
    methods: {
        searchDoctors: async function () {
            this.searching = true;
            this.doctors.splice(0);
            this.doctors.push(...await DATA_CLIENT.searchDoctors(this.searchModel));
            this.searching = false;
        },
        doctorClicked: function (doctor) {
            if (this.enabled) {
                this.selectedDoctor = doctor;
            }
        },
        createNewDoctor: function () {
            this.editingDoctor = {
                id: 0, personId: 0, person: {}};
            this.editing = true;
        },
        editDoctor: function () {

        },
        checkSave: function () {
            $.validator.unobtrusive.parse($(this.$el));
            $(this.$el).data('validator').settings.ignore = '';
            if ($(this.$el).valid()) {
                this.showPersonEditor = true;
            }
        },
        addPerson: function (person) {
            this.showPersonEditor = false;
            this.$refs.personId.value = person.id;
            this.editingDoctor.person = person;
            this.editingDoctor.personId = person.id;
        },
        saveDoctor: async function () {
            this.showSaveConfirmation = false;
            if (this.editingDoctor.id) {

            } else {
                this.editing = false;
                this.doctors.splice(0);
                this.doctors.push(await DATA_CLIENT.saveDoctor(this.editingDoctor));
                this.selectedDoctor = {
                    id: 0, personId: 0, person: {} };
                this.editingDoctor = {
                    id: 0, personId: 0, person: {} };
            }
        },
        cancelEditing: function () {
            this.editing = false;
            this.showCancelConfirmation = false;
        },
        addSpecialtyToDoctor: async function (specialty) {
            specialty.doctorId = this.selectedDoctor.id;
            return await DATA_CLIENT.addSpecialtyToDoctor(specialty);
        },
        editSpecialtyOfDoctor: async function (specialty) {
            return await DATA_CLIENT.editSpecialtyOfDoctor(specialty);
        },
        deleteSpecialtyFromDoctor: async function (specialty) {
            return await DATA_CLIENT.deleteSpecialtyFromDoctor(specialty);
        },
        addPhoneToDoctor: async function (phone) {
            phone.personId = this.selectedDoctor.personId;
            return await DATA_CLIENT.addPhoneToPerson(phone);
        },
        deletePhoneFromDoctor: async function (phone) {
            return await DATA_CLIENT.deletePhoneFromPerson(phone);
        },
        addEmailToDoctor: async function (email) {
            email.personId = this.selectedDoctor.personId;
            return await DATA_CLIENT.addEmailToPerson(email);
        },
        removeEmailFromDoctor: async function (email) {
            return await DATA_CLIENT.deleteEmailFromPerson(email);
        }
    },
    watch: {
        showPersonEditor: function (val) {
            this.enabled = !val;
        },
        showSaveConfirmation: function (val) {
            this.enabled = !val;
        },
        showCancelConfirmation: function (val) {
            this.enabled = !val;
        }
    },
    provide: function () {
        return {
            addSpecialty: this.addSpecialtyToDoctor,
            editSpecialty: this.editSpecialtyOfDoctor,
            deleteSpecialty: this.deleteSpecialtyFromDoctor,
            specialties: () => this.currentSpecialties,
            addPhone: this.addPhoneToDoctor,
            deletePhone: this.deletePhoneFromDoctor,
            addEmail: this.addEmailToDoctor,
            deleteEmail:this.removeEmailFromDoctor
        }
    }
});