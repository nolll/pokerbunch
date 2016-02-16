define(["vue", "text!components/report-form.html"],
    function(vue, html) {
        "use strict";

        return vue.extend({
            template: html,
            props: ['stack'],
            methods: {
                report: function () {
                    this.$dispatch('report', this.stack);
                },
                cancel: function () {
                    this.$dispatch('hide-forms');
                }
            }
        });
    }
);