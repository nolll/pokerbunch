define(["vue", "text!components/cashout-form.html", "validate"],
    function(vue, html, validate) {
        "use strict";

        return vue.extend({
            template: html,
            props: ['stack'],
            computed: {
                hasErrors: function () {
                    return this.stackError === null;
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