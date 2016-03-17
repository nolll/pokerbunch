define(["vue", "text!components/game-list-column/game-list-column.html"],
    function(vue, html) {
        "use strict";

        return vue.component("game-list-column", {
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
                    this.$dispatch("sort-by", this.name);
                }
            }
        });
    }
);