define(["jquery"],
    function ($) {
        "use strict";

        function HeadingNav(el) {
            var me = this;
            me.el = el;
            me.$el = $(me.el);
            me.$btn = me.$el.find('span');
            me.$content = me.$el.find('ul');

            me.$btn.click(function(){
                me.$content.toggle();
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