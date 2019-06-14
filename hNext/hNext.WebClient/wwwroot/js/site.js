// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

"use strict";

$.validator.addMethod("past", function (value, element, param) {
    return moment(value).isSameOrBefore(moment(), 'day');
});

$.validator.unobtrusive.adapters.addBool("past");

class DataClient {

    constructor(apiServer) {
        this._apiServer = apiServer;
    }

    async getRegionsByCountry(countryId) {
        return (await axios.get(`${this._apiServer}countries/${countryId}/regions`)).data;
    }

    async getCitiesByCountry(countryId) {
        return (await axios.get(`${this._apiServer}countries/${countryId}/cities`)).data;
    }

    async getCitiesByName(id, starts) {
        return (await axios.get(`${this._apiServer}countries/${id}/byname/${starts}`)).data;
    }

    async getDistrictsByRegion(regionId) {
        return (await axios.get(`${this._apiServer}regions/${regionId}/districts`)).data;
    }

    async getCitiesByRegion(regionId) {
        return (await axios.get(`${this._apiServer}regions/${regionId}/cities`)).data;
    }

    async getCitiesByDistrict(districtId) {
        return (await axios.get(`${this._apiServer}districts/${districtId}/cities`)).data;
    }

    async citiesStartWith(start) {
        return (await axios.get(`${this._apiServer}cities/startswith/${start}`)).data;
    }

    async getStreetsByCity(cityId) {
        return (await axios.get(`${this._apiServer}cities/${cityId}/streets`)).data;
    }

    async checkAddressExists(address) {
        return (await axios.post(
            `${this._apiServer}addresses/exists`,
            address
        )).data;
    }

    async savePhone(phone) {
        return (await axios.post(
            `${this._apiServer}phones`,
            phone
        )).data;
    }

    async editPhone(phone) {
        return (await axios.put(
            `${this._apiServer}phones/${phone.id}`,
            phone
        )).data;
    }

    async deletePhone(id) {
        return (await axios.delete(
            `${this._apiServer}phones/${id}`
        )).data;
    }

    async checkPhoneExists(number) {
        number = number.replace('+', '$$plus$$')
        return (await axios.get(
            `${this._apiServer}phones/exists/${number}`
        )).data;
    }

    async checkPhoneBelongsToOthers(id) {
        return (await axios.get(
            `${this._apiServer}phones/belongtoothers/${id}`
        )).data;
    }

    async addPhoneToPerson(personPhone) {
        return (await axios.post(
            `${this._apiServer}people/phone`,
            personPhone
        )).data;
    }

    async deletePhoneFromPerson(personPhone) {
        return (await axios.delete(
            `${this._apiServer}people/${personPhone.personId}/phone/${personPhone.phoneId}`
        )).data;
    }

    async editEmail(email) {
        return (await axios.put(
            `${this._apiServer}emails/${email.id}`,
            email
        )).data;
    }

    async checkEmailExists(address) {
        return (await axios.get(
            `${this._apiServer}emails/exists/${address}`
        )).data;
    }

    async addEmailToPerson(personEmail) {
        return (await axios.post(
            `${this._apiServer}people/email`,
            personEmail
        )).data;
    }

    async checkEmailBelongsToOthers(id) {
        return (await axios.get(
            `${this._apiServer}emails/belongtoothers/${id}`
        )).data;
    }

    async deleteEmailFromPerson(personEmail) {
        return (await axios.delete(
            `${this._apiServer}people/${personEmail.personId}/email/${personEmail.emailId}`
        )).data;
    }

    async addEmailToHospital(hospitalEmail) {
        return (await axios.post(
            `${this._apiServer}hospitals/${hospitalEmail.hospitalId}/emails`,
            hospitalEmail
        )).data;
    }

    async deleteEmailFromHospital(hospitalEmail) {
        return (await axios.delete(
            `${this._apiServer}hospitals/${hospitalEmail.hospitalId}/emails/${hospitalEmail.emailId}`
        )).data;
    }

    async addPhoneToHospital(hospitalPhone) {
        return (await axios.post(
            `${this._apiServer}hospitals/${hospitalPhone.hospitalId}/phones`,
            hospitalPhone
        )).data;
    }

    async deletePhoneFromHospital(hospitalPhone) {
        return (await axios.delete(
            `${this._apiServer}hospitals/${hospitalPhone.hospitalId}/phones/${hospitalPhone.phoneId}`
        )).data;
    }

    async addEmailToDepartment(departmentEmail) {
        return (await axios.post(
            `${this._apiServer}departments/${departmentEmail.departmentId}/emails/`,
            departmentEmail
        )).data;
    }

    async deleteEmailFromDepartment(departmentEmail) {
        return (await axios.delete(
            `${this._apiServer}departments/${departmentEmail.departmentId}/emails/${departmentEmail.emailId}`
        )).data;
    }

    async addPhoneToDeparment(departmentPhone) {
        return (await axios.post(
            `${this._apiServer}departments/${departmentPhone.departmentId}/phones`,
            departmentPhone
        )).data;
    }

    async deletePhoneFromDepartment(departmentPhone) {
        return (await axios.delete(
            `${this._apiServer}departments/${departmentPhone.departmentId}/phones/${this.departmentPhone.phoneId}`
        )).data;
    }

    async addDocument(document) {
        return (await axios.post(
            `${this._apiServer}documents`,
            document
        )).data;
    }

    async editDocument(document) {
        return (await axios.put(
            `${this._apiServer}documents/${document.id}`,
            document
        )).data;
    }

    async deleteDocument(documentId) {
        return (await axios.delete(
            `${this._apiServer}documents/${documentId}`
        )).data;
    }

    async documentExists(document) {
        return (await axios.get(
            `${this._apiServer}documents/exists/${document.documentTypeId}/${document.number}`
        )).data;
    }

    async addGuardian(guardian) {
        return (await axios.post(
            `${this._apiServer}guardians`,
            guardian
        )).data;
    }

    async editGuardian(guardian) {
        return (await axios.put(
            `${this._apiServer}guardians/${guardian.wardId}/${guardian.guardianId}`,
            guardian
        )).data;
    }

    async deleteGuardian(guardian) {
        return (await axios.delete(
            `${this._apiServer}guardians/${guardian.wardId}/${guardian.guardianId}`
        )).data;
    }

    async guardianExists(guardian) {
        return (await axios.post(
            `${this._apiServer}guardians/${guardian.wardId}/exists`,
            guardian
        )).data;
    }

    async guardianRelationExists(guardian) {
        return (await axios.get(
            `${this._apiServer}guardians/${guardian.wardId}/exists/${guardian.guardianId}`
        )).data;
    }

    async createPerson(person) {
        return (await axios.post(
            `${this._apiServer}people`,
            person
        )).data;
    }

    async savePerson(person) {
        return (await axios.put(
            `${this._apiServer}people/${person.id}`,
            person
        )).data;
    }

    async checkPersonExists(person) {
        return (await axios.post(
            `${this._apiServer}people/exists`,
            person
        )).data;
    }

    async searchPerson(name) {
        let nameArray = name.replace(/\s+/, '$');
        return (await axios.get(
            `${this._apiServer}people/search/${nameArray}`
        )).data;
    }

    async savePatient(patient) {
        return (await axios.post(
            `${this._apiServer}patients`,
            patient
        )).data;
    }

    async editPatient(patient) {
        return (await axios.put(
            `${this._apiServer}patients/${patient.id}`,
            patient
        )).data;
    }

    async searchPatients(model) {
        return (await axios.post(
            `${this._apiServer}patients/search`,
            model
        )).data;
    }

    async getHospitals() {
        return (await axios.get(
            `${this._apiServer}hospitals`
        )).data;
    }

    async saveHospital(hospital) {
        return (await axios.post(
            `${this._apiServer}hospitals`,
            hospital
        )).data;
    }

    async editHospital(hospital) {
        return (await axios.put(
            `${this._apiServer}hospitals/${hospital.id}`,
            hospital
        )).data;
    }

    async getDepartments() {
        return (await axios.get(
            `${this._apiServer}departments`
        )).data;
    }

    async saveDepartment(department) {
        return (await axios.post(
            `${this._apiServer}departments`,
            department
        )).data;
    }

    async editDepartment(department) {
        return (await axios.put(
            `${this._apiServer}departments/${department.id}`,
            department
        )).data;
    }

    async addSpecialtyToDpeartment(specialty) {
        return (await axios.post(
            `${this._apiServer}departments/${specialty.departmentId}/specialties/${specialty.specialtyId}`
        )).data;
    }

    async deleteSpecialtyFromDepartment(specialty) {
        return (await axios.delete(
            `${this._apiServer}departments/${specialty.departmentId}/specialties/${specialty.specialtyId}`
        )).data;
    }

    async saveDoctor(doctor) {
        return (await axios.post(
            `${this._apiServer}doctors`,
            doctor
        )).data;
    }

    async searchDoctors(model) {
        return (await axios.post(
            `${this._apiServer}doctors/search`,
            model
        )).data;
    }

    async addSpecialtyToDoctor(specialty) {
        return (await axios.post(
            `${this._apiServer}doctors/${specialty.doctorId}/specialties`,
            specialty
        )).data;
    }

    async editSpecialtyOfDoctor(specialty) {
        return (await axios.put(
            `${this._apiServer}doctors/${specialty.doctorId}/specialties/${specialty.specialtyId}`,
            specialty
        )).data;
    }

    async deleteSpecialtyFromDoctor(specialty) {
        return (await axios.delete(
            `${this._apiServer}doctors/${specialty.doctorId}/specialties/${specialty.specialtyId}`
        )).data;
    }
}

const DATA_CLIENT = new DataClient(API_SERVER);

Vue.use(Vuex);

const store = new Vuex.Store({
    modules: {
    },
    state: {
        enabled: true
    },
    mutations: {
        enable(state, show) {
            state.enabled = show;
        }
    }
});

Vue.filter('shortDate', (date) => {
    if (moment(date).isValid()) {
        let locale = window.navigator.userLanguage || window.navigator.language;
        moment.locale(locale);
        return moment(date).format('L');
    } else return '';
});