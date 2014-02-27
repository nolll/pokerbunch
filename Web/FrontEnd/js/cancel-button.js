define(["jquery"],
    function ($) {
        "use strict";

        function CancelButton(el) {
            var me = this;
            me.$el = $(el);
            me.cancelUrl = me.$el.data("cancel-url");

            me.$el.click(function(event) {
                event.preventDefault();
                if (me.cancelUrl !== undefined) {
                    location.href = me.cancelUrl;
                } else {
                    history.back();
                }
            });
        }

        function init() {
            return new CancelButton(this);
        }

        return {
            init: init
        };
    }
);