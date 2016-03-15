define(["jquery"],
    function ($) {
        "use strict";

        function SortableTable(el) {
            var me = this;
            me.el = el;
            me.$el = $(el);
            me.createForm();
        }

        SortableTable.prototype.createForm = function () {
            var me = this;
            var $div = me.createDiv();
            var $dropdown = me.createDropdown();
            $div.append($dropdown);
            var containerId = me.el.getAttribute("data-sort-control-container");
            var $container = $(document.getElementById(containerId));
            $container.prepend($div);
            $dropdown[0].addEventListener("change", function() {
                me.navigate(this.value);
            });
        };
        
        SortableTable.prototype.createDiv = function () {
            return $("<div class='table-list--sortable__sort-order-selector'><label for='sortorder'>Select Data:</label></div>");
        }

        SortableTable.prototype.createDropdown = function () {
            var me = this;
            var $dropdown = $("<select id='sortorder' name='sortorder'></select>");
            var $tableHeaders = me.$el.find("th");
            $tableHeaders.each(function () {
                var $th = $(this);
                var $link = $th.find("a");
                if ($link.length > 0) {
                    var $option = $("<option value='" + $link.attr("href") + "'>" + $link.text() + "</option>");
                    if ($th.hasClass("table-list--sortable__sort-column")) {
                        $option.attr("selected", "selected");
                    }
                    $dropdown.append($option);
                }
            });
            return $dropdown;
        }

        SortableTable.prototype.navigate = function (url) {
            location.href = url;
        };

        function init() {
            return new SortableTable(this);
        }

        return {
            init: init
        };
    }
);

