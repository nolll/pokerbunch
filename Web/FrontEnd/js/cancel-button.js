define(
    function () {
        "use strict";

        function CancelButton(el) {
            var me = this;
            me.cancelUrl = el.getAttribute("data-cancel-url");

            el.addEventListener('click', function(event) {
                event.preventDefault();
                if (me.cancelUrl !== null) {
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