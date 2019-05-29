"use strict";

if (!store.state['patient']) {
    const patientModule = {
        state: {
            patient: { id: 0 }
        },
        mutations: {
            setPatient(state, patient) {
                state.patient = patient;
            }
        },
        actions: {
            async getHospitalsAction(context) {
                let hospitals = await DATA_CLIENT.getHospitals();
                hospitals.forEach(h => context.commit('saveHospital', h));
            }
        }
    };
    store.registerModule('patient', patientModule);
}

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
            cityName:'',
            foundPatients: [],
            searching: false,
            showPersonEditor: false
        }
    },
    store,
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
        },
        'model.cityId': function (val) {
            if (val == '$reset') {
                this.cityName = '';
                this.cityId = '';
            }
        }
    },
    computed: {
        filteredCities: function () {
            return this.cities.filter(c => c.name.toLowerCase().startsWith(this.cityName.toLowerCase()));
        },
        selectedPatient: {
            get: function () {
                return this.$store.state.patient.patient;
            },
            set: function (patient) {
                this.$store.commit('setPatient', patient);
            }
        },
        enabled: {
            get: function() {
                return this.$store.state.enabled;
            },
            set: function(show) {
                this.$store.commit('enable', show);
            }
        }
    },
    methods: {
        searchPatients: async function () {
            this.searching = true;
            try {
                this.selectedPatient = { id: 0 };
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