define(["vue", "./game-list-column.html"],
    function(vue, html) {
        "use strict";

        return {
            template: html,
            props: ["name", "title", "orderBy", "isDefault"],
            computed: {
                sortingEnabled: function() {
                    return true;
                },
                isSelected: function() {
                    return this.name === this.orderBy;
                },
                sortColumnCssClass: function() {
                    return this.isSelected ? "table-list--sortable__sort-column" : "";
                },
                defaultColumnCssClass: function () {
                    return this.isDefault ? "table-list--sortable__base-column" : "";
                }
            },
            methods: {
                sort: function() {
                    this.eventHub.$emit("sort-by", this.name);
                }
            }
        };
    }
);