define(["jquery"],
    function ($) {
        "use strict";

        function HeadingNav(el) {
            var me = this;
            me.el = el;
            me.$el = $(me.el);
            me.$btn = me.$el.find('a');
            me.$content = me.$el.find('ul');
            me.isExpanded = false;

            me.$btn.click(function (event) {
                if (!me.isExpanded) {
                    event.preventDefault();
                    me.isExpanded = true;
                    me.$content.addClass("expanded");
                }
            });

            $('html').click(function () {
                if (me.isExpanded) {
                    me.isExpanded = false;
                    me.$content.removeClass("expanded");
                }
            });

            me.$el.click(function (event) {
                event.stopPropagation();
            });
        }

        function init() {
            return new HeadingNav(this);
        }

        return {
            init: init
        };
    }
);