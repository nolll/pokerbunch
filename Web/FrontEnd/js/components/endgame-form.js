define(["vue", "text!components/endgame-form.html"],
    function(vue, html) {
        "use strict";

        return vue.extend({
            template: html,
            methods: {
                endgame: function () {
                    this.$dispatch('endgame');
                },
                cancel: function () {
                    this.$dispatch('hide-forms');
                }
            }
        });
    }
);