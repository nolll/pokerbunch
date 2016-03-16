define(["vue", "text!components/top-list-row/top-list-row.html"],
    function(vue, html) {
        "use strict";

        return vue.component("top-list-row", {
            template: html,
            props: ["player"],
            created: function () {
                var x = 0;
            }
        });
    }
);