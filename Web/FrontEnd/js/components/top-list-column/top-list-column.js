define(["vue", "text!components/top-list-column/top-list-column.html"],
    function(vue, html) {
        "use strict";

        return vue.component("top-list-column", {
            template: html,
            props: ["name", "title", "orderBy"],
            computed: {
                sortingEnabled: function() {
                    return true;
                },
                isSelected: function() {
                    return this.name === this.orderBy;
                }
            },
            methods: {
                sort: function() {
                    this.$dispatch('sort-by', this.name);
                }
            }
        });
    }
);