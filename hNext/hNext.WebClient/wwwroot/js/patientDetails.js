"use strict";

Vue.component('PatientDetails', {
    template: '#patient-details-template',
    store,
    computed: {
        firstName: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person)
                ? this.$store.state.patient.patient.person.firstName : '';
        },
        familyName: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person)
                ? this.$store.state.patient.patient.person.familyName : '';
        },
        patronimic: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person)
                ? this.$store.state.patient.patient.person.patronimic : '';
        },
        gender: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person && this.$store.state.patient.patient.person.gender)
                ? this.$store.state.patient.patient.person.gender.name : '';
        },
        dateOfBirth: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person)
                ? this.$store.state.patient.patient.person.dateOfBirth : '';
        },
        countryOfBirth: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person && this.$store.state.patient.patient.person.countryOfBirth)
                ? this.$store.state.patient.patient.person.countryOfBirth.name : '';
        },
        placeOfBirth: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person && this.$store.state.patient.patient.person.placeOfBirth)
                ? this.$store.state.patient.patient.person.placeOfBirth.name : '';
        },
        addressCountry: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person
                && this.$store.state.patient.patient.person.address && this.$store.state.patient.patient.person.address.country)
                ? this.$store.state.patient.patient.person.address.country.name : '';
        },
        addressRegion: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person
                && this.$store.state.patient.patient.person.address && this.$store.state.patient.patient.person.address.region)
                ? this.$store.state.patient.patient.person.address.region.name : '';
        },
        addressDistrict: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person
                && this.$store.state.patient.patient.person.address && this.$store.state.patient.patient.person.address.district)
                ? this.$store.state.patient.patient.person.address.district.name : '';
        },
        addressCityType: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person
                && this.$store.state.patient.patient.person.address && this.$store.state.patient.patient.person.address.city
                && this.$store.state.patient.patient.person.address.city.cityType)
                ? this.$store.state.patient.patient.person.address.city.cityType.name : '';
        },
        addressCity: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person
                && this.$store.state.patient.patient.person.address && this.$store.state.patient.patient.person.address.city)
                ? this.$store.state.patient.patient.person.address.city.name : '';
        },
        addressStreetType: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person
                && this.$store.state.patient.patient.person.address && this.$store.state.patient.patient.person.address.street
                && this.$store.state.patient.patient.person.address.street.streetType)
                ? this.$store.state.patient.patient.person.address.street.streetType.name : '';
        },
        addressStreet: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person
                && this.$store.state.patient.patient.person.address && this.$store.state.patient.patient.person.address.street)
                ? this.$store.state.patient.patient.person.address.street.name : '';
        },
        addressBuilding: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person && this.$store.state.patient.patient.person.address)
                ? this.$store.state.patient.patient.person.address.building : '';
        },
        addressApartment: function () {
            return (this.$store.state.patient.patient && this.$store.state.patient.patient.person && this.$store.state.patient.patient.person.address)
                ? this.$store.state.patient.patient.person.address.apartment : '';
        }
    }
});