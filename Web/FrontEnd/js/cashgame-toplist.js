define(["jquery"],
    function ($) {
        "use strict";

        function Toplist(el) {
            var me = this;
            me.$el = $(el);
            me.createForm();
        }

        Toplist.prototype.createForm = function () {
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
            me.$el.parent().prepend($div);
            $dropdown.on("change", function() {
                me.navigate(this.value);
            });
        };

        Toplist.prototype.navigate = function (url) {
            location.href = url;
        };

        function init() {
            return new Toplist(this);
        }

        return {
            init: init
        };
    }
);

