define(["jquery"],
    function ($) {
        "use strict";

        function HeadingNav(el) {
            var me = this;
            me.el = el;
            me.$el = $(me.el);
            me.isExpanded = false;

            el.addEventListener('click', function (e) {
                if (!me.isExpanded) {
                    e.preventDefault();
                    me.isExpanded = true;
                    me.$el.addClass("is-expanded");
                }
                event.stopPropagation();
            });

            $('html').click(function () {
                if (me.isExpanded) {
                    me.isExpanded = false;
                    me.$el.removeClass("is-expanded");
                }
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