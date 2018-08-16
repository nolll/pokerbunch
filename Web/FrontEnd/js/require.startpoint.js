define(["dom-hookup", "components"],
    function (domHookup, components) {

        function domReady(callback) {
            document.readyState === "interactive" || document.readyState === "complete" ? callback() : document.addEventListener("DOMContentLoaded", callback);
        }

        function init() {
            domReady(function () {
                domHookup.init();
                components.init();
            });
        }

        return {
            init: init
        };
    }
);