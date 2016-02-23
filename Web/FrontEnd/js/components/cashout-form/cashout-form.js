define(["vue", "text!components/cashout-form/cashout-form.html", "validate"],
    function(vue, html, validate) {
        "use strict";

        return vue.component("cashout-form", {
            template: html,
            props: ['stack', "isActive"],
            computed: {
                hasErrors: function () {
                    return this.stackError === null;
                }
            },
            watch: {
                'isActive': function (val) {
                    if (val) {
                        this.$els.stack.focus();
                    }
                }
            },
            methods: {
                cashout: function () {
                    this.validateForm();
                    if (!this.hasErrors)
                        this.$dispatch('cashout', this.stack);
                },
                cancel: function () {
                    this.$dispatch('hide-forms');
                },
                focus: function (event) {
                    event.target.select();
                },
                validateForm: function () {
                    this.clearErrors();
                    if (validate.intRange(this.stack, 0))
                        this.stackError = "Stack can't be negative";
                },
                clearErrors: function () {
                    this.stackError = null;
                }
            },
            data: function () {
                return {
                    stackError: null
                }
            }
        });
    }
);