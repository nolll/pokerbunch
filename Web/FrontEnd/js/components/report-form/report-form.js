define(["vue", "./report-form.html", "../../validate", "../../forms"],
    function(vue, html, validate, forms) {
        "use strict";

        return {
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
                        this.$refs.stack.focus();
                    }
                }
            },
            methods: {
                report: function () {
                    this.validateForm();
                    if (!this.hasErrors)
                        this.eventHub.$emit('report', this.stack);
                },
                cancel: function () {
                    this.eventHub.$emit('hide-forms');
                },
                focus: function (event) {
                    forms.selectAll(event.target);
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
        };
    }
);