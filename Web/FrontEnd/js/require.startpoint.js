define(["jquery", "require.init", "components"],
    function ($, requireInit, components) {
        function init() {
            $(document).ready(function () {
                components.init();
                requireInit.init($(document));
            });
        }
        return { init: init };
    }
);