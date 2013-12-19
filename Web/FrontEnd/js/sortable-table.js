define(["jquery"],
    function ($) {
        "use strict";

        function SortableTable(el) {
            var me = this;
            me.$el = $(el);
            me.createForm();
        }

        SortableTable.prototype.createForm = function () {
            var me = this;
            var $div = $("<div class='sort-order-selector'><label for='sortorder'>Select Data:</label></div>");
            var $dropdown = $("<select id='sortorder' name='sortorder'></select>");
            var $tableHeaders = me.$el.find("th");
            $tableHeaders.each(function () {
                var $th = $(this);
                var $link = $th.find("a");
                if ($link.length > 0) {
                    var $option = $("<option value='" + $link.attr("href") + "'>" + $link.text() + "</option>");
                    if ($th.hasClass("sort-column")) {
                        $option.attr("selected", "selected");
                    }
                    $dropdown.append($option);
                }
            });
            $div.append($dropdown);
            var containerId = me.$el.attr("data-sort-control-container");
            var $container = $(document.getElementById(containerId));
            $container.prepend($div);
            $dropdown.on("change", function() {
                me.navigate(this.value);
            });
        };

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

