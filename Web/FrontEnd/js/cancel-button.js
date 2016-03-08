define([],
    function () {
        "use strict";

        function CancelButton(el) {
            var me = this;
            me.cancelUrl = el.getAttribute("data-cancel-url");

            el.addEventListener('click', function(event) {
                event.preventDefault();
                if (me.cancelUrl !== undefined) {
                    location.href = me.cancelUrl;
                } else {
                    history.back();
                }
            }, false);
        }

        function init() {
            return new CancelButton(this);
        }

        return {
            init: init
        };
    }
);