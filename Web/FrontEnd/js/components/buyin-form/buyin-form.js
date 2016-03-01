define(["vue", "text!components/buyin-form/buyin-form.html", "validate", "forms"],
    function(vue, html, validate, forms) {
        "use strict";

        return vue.component("buyin-form", {
            template: html,
            props: ['stack', 'amount', "isActive"],
            computed: {
                hasErrors: function() {
                    return this.buyinError === null && this.stackError === null;
                }
            },
            watch: {
                'isActive': function (val) {
                    if (val) {
                        this.$els.buyin.focus();
                    }
                }
            },
            methods: {
                buyin: function () {
                    this.validateForm();
                    if(!this.hasErrors)
                        this.$dispatch('buyin', this.amount, this.stack);
                },
                cancel: function () {
                    this.$dispatch('hide-forms');
                },
                focus: function (event) {
                    forms.selectAll(event.target);
                },
                validateForm: function () {
                    this.clearErrors();
                    if (validate.intRange(this.amount, 1))
                        this.buyinError = "Buyin must be greater than zero";
                    if (validate.intRange(this.stack, 0))
                        this.stackError = "Stack can't be negative";
                },
                clearErrors: function() {
                    this.buyinError = null;
                    this.stackError = null;
                }
            },
            data: function() {
                return {
                    buyinError: null,
                    stackError: null
                }
            }
        });
    }
);