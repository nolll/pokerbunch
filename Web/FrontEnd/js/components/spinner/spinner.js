define(["vue", "text!components/spinner/spinner.html"],
    function(vue, html) {
        "use strict";

        return vue.component("spinner", {
            template: html
        });
    }
);