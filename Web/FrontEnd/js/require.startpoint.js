define(["jquery", "require.init", "components"],
    function ($, requireInit, components) {
        function init() {
            $(document).ready(function () {
                requireInit.init($(document));
                components.init();
            });
        }
        return {
            init: init
        };
    }
);