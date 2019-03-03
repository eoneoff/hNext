"use strict";

function patientDetailsModel(id) {
    return new Vue({
        el: `#${id}`,
        computed: {
            firstName: function () {
                return (store.state.patient && store.state.patient.person)
                    ? store.state.patient.person.firstName : '';
            },
            familyName: function () {
                return (store.state.patient && store.state.patient.person)
                    ? store.state.patient.person.familyName : '';
            },
            patronimic: function () {
                return (store.state.patient && store.state.patient.person)
                    ? store.state.patient.person.patronimic : '';
            },
            gender: function () {
                return (store.state.patient && store.state.patient.person && store.state.patient.person.gender)
                    ? store.state.patient.person.gender.name : '';
            },
            dateOfBirth: function () {
                return (store.state.patient && store.state.patient.person)
                    ? store.state.patient.person.dateOfBirth : '';
            },
            countryOfBirth: function() {
                return (store.state.patient && store.state.patient.person && store.state.patient.person.countryOfBirth)
                    ? store.state.patient.person.countryOfBirth.name : '';
            },
            placeOfBirth: function() {
                return (store.state.patient && store.state.patient.person && store.state.patient.person.placeOfBirth)
                    ? store.state.patient.person.placeOfBirth.name : '';
            },
            addressCountry: function () {
                return (store.state.patient && store.state.patient.person
                    && store.state.patient.person.address && store.state.patient.person.address.country)
                    ? store.state.patient.person.address.country.name : '';
            },
            addressRegion: function () {
                return (store.state.patient && store.state.patient.person
                    && store.state.patient.person.address && store.state.patient.person.address.region)
                    ? store.state.patient.person.address.region.name : '';
            },
            addressDistrict: function () {
                return (store.state.patient && store.state.patient.person
                    && store.state.patient.person.address && store.state.patient.person.address.district)
                    ? store.state.patient.person.address.district.name : '';
            },
            addressCityType: function () {
                return (store.state.patient && store.state.patient.person
                    && store.state.patient.person.address && store.state.patient.person.address.city
                    && store.state.patient.person.address.city.cityType)
                    ? store.state.patient.person.address.city.cityType.name : '';
            },
            addressCity: function () {
                return (store.state.patient && store.state.patient.person
                    && store.state.patient.person.address && store.state.patient.person.address.city)
                    ? store.state.patient.person.address.city.name : '';
            },
            addressStreetType: function () {
                return (store.state.patient && store.state.patient.person
                    && store.state.patient.person.address && store.state.patient.person.address.street
                    && store.state.patient.person.address.street.streetType)
                    ? store.state.patient.person.address.street.streetType.name : '';
            },
            addressStreet: function () {
                return (store.state.patient && store.state.patient.person
                    && store.state.patient.person.address && store.state.patient.person.address.street)
                    ? store.state.patient.person.address.street.name : '';
            },
            addressBuilding: function () {
                return (store.state.patient && store.state.patient.person && store.state.patient.person.address)
                    ? store.state.patient.person.address.building : '';
            },
            addressApartment: function () {
                return (store.state.patient && store.state.patient.person && store.state.patient.person.address)
                    ? store.state.patient.person.address.apartment : '';
            }
        }
    });
}