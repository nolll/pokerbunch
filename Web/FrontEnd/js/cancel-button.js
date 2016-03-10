define(
    function () {
        "use strict";

        function init() {
            var me = this;
            me.cancelUrl = this.getAttribute("data-cancel-url");

            this.addEventListener('click', function (event) {
                event.preventDefault();
                if (me.cancelUrl !== null) {
                    location.href = me.cancelUrl;
                } else {
                    history.back();
                }
            });
        }

        return {
            init: init
        };
    }
);