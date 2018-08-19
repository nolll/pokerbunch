define(["vue", "./top-list-column.html"],
    function(vue, html) {
        "use strict";

        return {
            template: html,
            props: ["name", "title", "orderBy"],
            computed: {
                sortingEnabled: function() {
                    return true;
                },
                isSelected: function() {
                    return this.name === this.orderBy;
                },
                sortColumnCssClass: function() {
                    return this.isSelected ? "table-list--sortable__sort-column" : "";
                }
            },
            methods: {
                sort: function() {
                    this.eventHub.$emit('sort-by', this.name);
                }
            }
        };
    }
);