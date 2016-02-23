define(["vue", "text!components/report-form/report-form.html", "validate"],
    function(vue, html, validate) {
        "use strict";

        return vue.component("report-form", {
            template: html,
            props: ['stack', 'isActive'],
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
                report: function () {
                    this.validateForm();
                    if (!this.hasErrors)
                        this.$dispatch('report', this.stack);
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