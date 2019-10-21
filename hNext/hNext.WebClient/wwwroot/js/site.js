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
        this._client = axios.create({
            baseURL: apiServer
        });
    }

    async getRegionsByCountry(countryId) {
        return (await this._client.get(`countries/${countryId}/regions`)).data;
    }

    async getCitiesByCountry(countryId) {
        return (await this._client.get(`countries/${countryId}/cities`)).data;
    }

    async getCitiesByName(id, starts) {
        return (await this._client.get(`countries/${id}/byname/${starts}`)).data;
    }

    async getDistrictsByRegion(regionId) {
        return (await this._client.get(`regions/${regionId}/districts`)).data;
    }

    async getCitiesByRegion(regionId) {
        return (await this._client.get(`regions/${regionId}/cities`)).data;
    }

    async getCitiesByDistrict(districtId) {
        return (await this._client.get(`districts/${districtId}/cities`)).data;
    }

    async citiesStartWith(start) {
        return (await this._client.get(`cities/startswith/${start}`)).data;
    }

    async getStreetsByCity(cityId) {
        return (await this._client.get(`cities/${cityId}/streets`)).data;
    }

    async checkAddressExists(address) {
        return (await this._client.post(
            `addresses/exists`,
            address
        )).data;
    }

    async savePhone(phone) {
        return (await this._client.post(
            `phones`,
            phone
        )).data;
    }

    async editPhone(phone) {
        return (await this._client.put(
            `phones/${phone.id}`,
            phone
        )).data;
    }

    async deletePhone(id) {
        return (await this._client.delete(
            `phones/${id}`
        )).data;
    }

    async checkPhoneExists(number) {
        number = number.replace('+', '$$plus$$');
        return (await this._client.get(
            `phones/exists/${number}`
        )).data;
    }

    async checkPhoneBelongsToOthers(id) {
        return (await this._client.get(
            `phones/belongtoothers/${id}`
        )).data;
    }

    async addPhoneToPerson(personPhone) {
        return (await this._client.post(
            `people/phone`,
            personPhone
        )).data;
    }

    async deletePhoneFromPerson(personPhone) {
        return (await this._client.delete(
            `people/${personPhone.personId}/phone/${personPhone.phoneId}`
        )).data;
    }

    async editEmail(email) {
        return (await this._client.put(
            `emails/${email.id}`,
            email
        )).data;
    }

    async checkEmailExists(address) {
        return (await this._client.get(
            `emails/exists/${address}`
        )).data;
    }

    async addEmailToPerson(personEmail) {
        return (await this._client.post(
            `people/email`,
            personEmail
        )).data;
    }

    async checkEmailBelongsToOthers(id) {
        return (await this._client.get(
            `emails/belongtoothers/${id}`
        )).data;
    }

    async deleteEmailFromPerson(personEmail) {
        return (await this._client.delete(
            `people/${personEmail.personId}/email/${personEmail.emailId}`
        )).data;
    }

    async addEmailToHospital(hospitalEmail) {
        return (await this._client.post(
            `hospitals/${hospitalEmail.hospitalId}/emails`,
            hospitalEmail
        )).data;
    }

    async deleteEmailFromHospital(hospitalEmail) {
        return (await this._client.delete(
            `hospitals/${hospitalEmail.hospitalId}/emails/${hospitalEmail.emailId}`
        )).data;
    }

    async addPhoneToHospital(hospitalPhone) {
        return (await this._client.post(
            `hospitals/${hospitalPhone.hospitalId}/phones`,
            hospitalPhone
        )).data;
    }

    async deletePhoneFromHospital(hospitalPhone) {
        return (await this._client.delete(
            `hospitals/${hospitalPhone.hospitalId}/phones/${hospitalPhone.phoneId}`
        )).data;
    }

    async addEmailToDepartment(departmentEmail) {
        return (await this._client.post(
            `departments/${departmentEmail.departmentId}/emails/`,
            departmentEmail
        )).data;
    }

    async deleteEmailFromDepartment(departmentEmail) {
        return (await this._client.delete(
            `departments/${departmentEmail.departmentId}/emails/${departmentEmail.emailId}`
        )).data;
    }

    async addPhoneToDeparment(departmentPhone) {
        return (await this._client.post(
            `departments/${departmentPhone.departmentId}/phones`,
            departmentPhone
        )).data;
    }

    async deletePhoneFromDepartment(departmentPhone) {
        return (await this._client.delete(
            `departments/${departmentPhone.departmentId}/phones/${this.departmentPhone.phoneId}`
        )).data;
    }

    async addDocument(document) {
        return (await this._client.post(
            `documents`,
            document
        )).data;
    }

    async editDocument(document) {
        return (await this._client.put(
            `documents/${document.id}`,
            document
        )).data;
    }

    async deleteDocument(documentId) {
        return (await this._client.delete(
            `documents/${documentId}`
        )).data;
    }

    async documentExists(document) {
        return (await this._client.get(
            `documents/exists/${document.documentTypeId}/${document.number}`
        )).data;
    }

    async addGuardian(guardian) {
        return (await this._client.post(
            `guardians`,
            guardian
        )).data;
    }

    async editGuardian(guardian) {
        return (await this._client.put(
            `guardians/${guardian.wardId}/${guardian.guardianId}`,
            guardian
        )).data;
    }

    async deleteGuardian(guardian) {
        return (await this._client.delete(
            `guardians/${guardian.wardId}/${guardian.guardianId}`
        )).data;
    }

    async guardianExists(guardian) {
        return (await this._client.post(
            `guardians/${guardian.wardId}/exists`,
            guardian
        )).data;
    }

    async guardianRelationExists(guardian) {
        return (await this._client.get(
            `guardians/${guardian.wardId}/exists/${guardian.guardianId}`
        )).data;
    }

    async createPerson(person) {
        return (await this._client.post(
            `people`,
            person
        )).data;
    }

    async savePerson(person) {
        return (await this._client.put(
            `people/${person.id}`,
            person
        )).data;
    }

    async checkPersonExists(person) {
        return (await this._client.post(
            `people/exists`,
            person
        )).data;
    }

    async searchPerson(name) {
        let nameArray = name.replace(/\s+/, '$');
        return (await this._client.get(
            `people/search/${nameArray}`
        )).data;
    }

    async savePatient(patient) {
        return (await this._client.post(
            `patients`,
            patient
        )).data;
    }

    async editPatient(patient) {
        return (await this._client.put(
            `patients/${patient.id}`,
            patient
        )).data;
    }

    async searchPatients(model) {
        return (await this._client.post(
            `patients/search`,
            model
        )).data;
    }

    async getHospitals() {
        return (await this._client.get(
            `hospitals`
        )).data;
    }

    async saveHospital(hospital) {
        return (await this._client.post(
            `hospitals`,
            hospital
        )).data;
    }

    async editHospital(hospital) {
        return (await this._client.put(
            `hospitals/${hospital.id}`,
            hospital
        )).data;
    }

    async getDepartments() {
        return (await this._client.get(
            `departments`
        )).data;
    }

    async saveDepartment(department) {
        return (await this._client.post(
            `departments`,
            department
        )).data;
    }

    async editDepartment(department) {
        return (await this._client.put(
            `departments/${department.id}`,
            department
        )).data;
    }

    async addSpecialtyToDpeartment(specialty) {
        return (await this._client.post(
            `departments/${specialty.departmentId}/specialties/${specialty.specialtyId}`
        )).data;
    }

    async deleteSpecialtyFromDepartment(specialty) {
        return (await this._client.delete(
            `departments/${specialty.departmentId}/specialties/${specialty.specialtyId}`
        )).data;
    }

    async getDoctors() {
        return (await this._client('doctors')).data;
    }

    async saveDoctor(doctor) {
        return (await this._client.post(
            `doctors`,
            doctor
        )).data;
    }

    async searchDoctors(model) {
        return (await this._client.post(
            `doctors/search`,
            model
        )).data;
    }

    async addSpecialtyToDoctor(specialty) {
        return (await this._client.post(
            `doctors/${specialty.doctorId}/specialties`,
            specialty
        )).data;
    }

    async editSpecialtyOfDoctor(specialty) {
        return (await this._client.put(
            `doctors/${specialty.doctorId}/specialties/${specialty.specialtyId}`,
            specialty
        )).data;
    }

    async deleteSpecialtyFromDoctor(specialty) {
        return (await this._client.delete(
            `doctors/${specialty.doctorId}/specialties/${specialty.specialtyId}`
        )).data;
    }

    async addPositionToDoctor(position) {
        return (await this._client.post(
            `doctors/${position.doctorId}/positions`,
            position
        )).data;
    }

    async editPositionOfDoctor(position) {
        return (await this._client.put(
            `doctors/${position.doctorId}/positions/${position.id}`,
            position
        )).data;
    }

    async deletePositionFromDoctor(position) {
        return (await this._client.delete(
            `doctors/${position.doctorId}/positions/${position.id}`
        )).data;
    }

    async addDiplomaToDoctor(diploma) {
        return (await this._client.post(
            `doctors/${diploma.doctorId}/diplomas`,
            diploma
        )).data;
    }

    async editDiplomaOfDoctor(diploma) {
        return (await this._client.put(
            `doctors/${diploma.doctorId}/diplomas/${diploma.id}`,
            diploma
        )).data;
    }

    async removeDiplomaFromDoctor(diploma) {
        return (await this._client.delete(
            `doctors/${diploma.doctorId}/diplomas/${diploma.id}`
        )).data;
    }

    async addCaseHistory(history) {
        return (await this._client.post(
            `casehistories`,
            history
        )).data;
    }

    async getCaseHistory(id) {
        return (await this._client.get(
            `casehistories/${id}`
        )).data;
    }

    async getCaseHistoryInfo(id) {
        return (await this._client.get(
            `casehistories/${id}/info`
        )).data;
    }
    
    async addDiagnosys(diagnosys) {
        return (await this_client.post(
            'diagnoses',
            diagnosys
        )).data;
    }

    async getDiagnosesOfPatient(patientId) {
        return (await this._client.get(
            `patients/${patiendId}/diagnoses`
        )).data;
    }

    async addDiagnosysToPatient(diagnosys) {
        return (await this._client.post(
            `patients/${diagnosys.patientId}/diagnoses`,
            diagnosys
        )).data;
    }

    async removeDiagnosysFromPatient(diagnosys) {
        return (await this._client.delete(
            `patients/${diagnosys.patientId}/diagnoses/${diagnosys.diagnosysId}`
        )).data;
    }

    async addDiagnosysToCaseHistory(diagnosys) {
        return (await this._client.post(
            `casehistories/${diagnosys.caseHistoryId}/diagnoses`,
            diagnosys
        )).data;
    }

    async removeDiagnosysFromCaseHistory(diagnosis) {
        return (await this._client.delete(
            `casehistories/${diagnosis.caseHistoryId}/diagnoses/${diagnosis.diagnosysId}`
        )).data;
    }

    async addAdmissionToCaseHistory(admission) {
        return (await this._client.post(
            `casehistories/${admission.caseHistoryId}/admissions`,
            admission
        )).data;
    }

    async editAdmissionOfCaseHistory(admission) {
        return await this._client.put(
            `casehistory/${admission.caseHistoryId}/admissions/${admission.id}`,
            admission
        ).data;
    }

    async getDiagnoses() {
        return (await this._client.get(
            'diagnoses'
        )).data;
    }

    async addDiagnosys(diagnosys) {
        return (await this._client.post(
            'diagnoses', diagnosys
        )).data;
    }

    async getICD() {
        return (await this._client.get('icd')).data;
    }

    async searchICD(icd) {
        return (await this._client.post(
            'icd/search',
            icd
        )).data;
    }

    async getSpecialties() {
        return (await this._client.get('specialties')).data;
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