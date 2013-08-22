define(["jquery", "require.init"],
    function ($, requireInit) {
        function init() {
            $(document).ready(function () {
                requireInit.init($(document));
            });
        }
        return { init: init };
    }
);