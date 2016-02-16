define(["vue", "text!components/cashout-form.html"],
    function(vue, html) {
        "use strict";

        return vue.extend({
            template: html,
            props: ['stack'],
            methods: {
                cashout: function () {
                    this.$dispatch('cashout', this.stack);
                },
                cancel: function () {
                    this.$dispatch('hide-forms');
                }
            }
        });
    }
);