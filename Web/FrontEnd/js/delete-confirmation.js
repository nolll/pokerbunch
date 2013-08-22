define(["jquery"],
    function ($) {
        "use strict";

        function init() {
            var me = this,
                $me = $(this),
                message = $me.data("message");
            if(!message){
                message = "Delete?";
            }
            $me.click(function() {
                return window.confirm(message);
            });
        }

        return {
            init: init
        };
    }
);