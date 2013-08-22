define(["jquery"],
    function ($) {
        "use strict";

        function FocusTextSelector(el) {
            var me = this;
            me.$el = $(el);

            me.$el.focus(function(){
                if(this.setSelectionRange){
                    this.setSelectionRange(0, 999);
                }
            });
            me.$el.mouseup(function(event){
                event.preventDefault();
            });
        }

        function init() {
            return new FocusTextSelector(this);
        }

        return {
            init: init
        };
    }
);