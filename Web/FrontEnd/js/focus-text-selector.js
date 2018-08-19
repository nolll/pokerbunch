define(["./forms"],
    function (forms) {
        "use strict";

        function init() {
            this.addEventListener('focus', function (e) {
                forms.selectAll(e.target);
            });
        }

        return {
            init: init
        };
    }
);