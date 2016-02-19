define(["vue", "text!components/spinner.html"],
    function(vue, html) {
        "use strict";

        return vue.extend({
            template: html
        });
    }
);