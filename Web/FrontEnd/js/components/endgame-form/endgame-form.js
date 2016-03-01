define(["vue", "text!components/endgame-form/endgame-form.html"],
    function(vue, html) {
        "use strict";

        return vue.component("endgame-form", {
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