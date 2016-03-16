define(["vue", "text!components/top-list-table/top-list-table.html"],
    function(vue, html) {
        "use strict";

        return vue.component("top-list-table", {
            template: html,
            props: ["jsonContainer"],
            created: function () {
                var x = 0;
            },
            data: function () {
                var jsonElement = document.getElementById(this.jsonContainer);
                return JSON.parse(jsonElement.innerHTML);
            }
        });
    }
);