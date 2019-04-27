"use strict";

Vue.component("GuardianEditor", {
    template: '#guardian-editor-template',
    props: ['initialGuardian', 'level'],
    data: function () {
        return {
            guardian: $.extend(true, {}, this.initialGuardian),
            searchString:'',
            foundPeople: [],
            enabled: true,
            showSaveConfirmation: false,
            showChangeConfirmation: false,
            showPersonEditor:false
        }
    },
    methods: {
        save: async function () {
            if ($(this.$el).valid()) {
                this.enabled = false;
                let existing = await DATA_CLIENT.guardianRelationExists(this.guardian);
                if (existing) {
                    this.showChangeConfirmation = true;
                } else {
                    this.showSaveConfirmation = true;
                }
            }
        },
        saveConfirmed: async function () {
            this.$emit('save', this.guardian);
        },
        saveCancelled: function () {
            this.showSaveConfirmation = false;
            this.showChangeConfirmation = false;
            this.enabled = true;
        },            
        searchPerson: async function () {
            this.enabled = false;
            this.foundPeople = (await DATA_CLIENT.searchPerson(this.searchString))
                .filter(p => p.id != this.guardian.wardId);
            this.enabled = true;
        },
        selectPerson: function (person) {
            if (this.enabled) {
                this.guardian.guardian = person;
            }
        },
        createPerson: function () {
            this.enabled = false;
            this.showPersonEditor = true;
        },
        newPersonAdded: function (person) {
            this.guardian.guardian = person;
            this.foundPeople = [];
            this.enabled = true;
            this.showPersonEditor = false;
        },
        personEditorQuitted: function () {
            this.enabled = true;
            this.showPersonEditor = false;
        }
    },
    watch: {
        'guardian.guardian': function() {
            this.$refs.guardianId.value = this.guardian.guardian.id;
            $(this.$refs.guardianId).valid();
        }
    },
    mounted: function () {
        $.validator.unobtrusive.parse($(this.$el));
        $(this.$el).data('validator').settings.ignore = '';
        this.$refs.guardianId.value = this.guardian.guardian.id;
    }
});