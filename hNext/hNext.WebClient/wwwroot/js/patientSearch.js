"use strict";

Vue.component('PatientSearch', {
    template: '#patient-search-template',
    data: function () {
        return {
            model: {
                name: "",
                yearOfBirth: "",
                regionId: "",
                districtId: "",
                cityId: ""
            },
            districts: [],
            cities: [],
            foundPatients: [],
            searching: false,
            showPersonEditor: false
        }
    },
    watch: {
        'model.regionId': async function (val) {
            this.districts.splice(0);
            this.cities.splice(0);
            if (val != "") {
                this.districts.push(...await DATA_CLIENT.getDistrictsByRegion(val));
                this.cities.push(...await DATA_CLIENT.getCitiesByRegion(val));
            }
            this.model.districtId = "";
            this.model.cityId = "";
        },
        'model.districtId': async function (val) {
            this.cities.splice(0);
            if (val != "") {
                this.cities.push(...await DATA_CLIENT.getCitiesByDistrict(val));
            }
            this.model.cityId = "";
        }
    },
    computed: {
        selectedPatient: {
            get: function () {
                return store.state.patient;
            },
            set: function (patient) {
                store.commit('setPatient', patient);
            }
        },
        enabled: {
            get: function() {
                return store.state.enabled;
            },
            set: function(show) {
                store.commit('enable', show);
            }
        }
    },
    methods: {
        searchPatients: async function () {
            this.searching = true;
            try {
                store.state.patient = { id: 0 };
                this.foundPatients.splice(0);
                this.foundPatients.push(...await DATA_CLIENT.searchPatients(this.model));
            }
            finally {
                this.searching = false;
            }
        },
        selectPatient: function (patient) {
            if (this.enabled) {
                this.selectedPatient = patient;
            }
        },
        createNewPatient: function () {
            this.selectedPatient = { id: 0 };
            this.showPatientEditor();
        },
        showPatientEditor: function() {
            this.showPersonEditor = true;
            this.enabled = false;
        },
        savePatient: async function(person) {
            this.patientEditorQuited();
            if (this.selectedPatient.personId) {
                this.selectedPatient.person = person;
            } else {
                let patient = {  personId: person.id };
                this.foundPatients.splice(0);
                patient = await DATA_CLIENT.savePatient(patient);
                this.foundPatients.push(patient);
                this.selectedPatient = patient;
            }
        },
        patientEditorQuited: function () {
            this.enabled = true;
            this.showPersonEditor = false;
        }
    }
});