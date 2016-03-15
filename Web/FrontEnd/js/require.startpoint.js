define(["require.init", "components"],
    function (requireInit, components) {

        function domReady(callback) {
            document.readyState === "interactive" || document.readyState === "complete" ? callback() : document.addEventListener("DOMContentLoaded", callback);
        }

        function init() {
            domReady(function () {
                requireInit.init();
                components.init();
            });
        }

        return {
            init: init
        };
    }
);