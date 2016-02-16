define(["vue", "text!components/buyin-form.html"],
    function(vue, html) {
        "use strict";

        return vue.extend({
            template: html,
            props: ['stack', 'amount'],
            methods: {
                buyin: function () {
                    this.$dispatch('buyin', this.amount, this.stack);
                },
                cancel: function () {
                    this.$dispatch('hide-forms');
                }
            }
        });
    }
);