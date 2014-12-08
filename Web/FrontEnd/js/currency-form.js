define(["jquery"],
    function ($) {
        "use strict";

        function init() {
            var $me = $(this);
            updateLayouts($me);
            $me.blur(function(){
                updateLayouts($me);
            });
        }

        function updateLayouts($symbol){
            var symbol = $symbol.val();
            $("#currencylayout option").each(function(){
                $(this).html($(this).val().replace("{SYMBOL}", symbol).replace("{AMOUNT}", "123"));
            });
        }

        return {
            init: init
        };
    }
);