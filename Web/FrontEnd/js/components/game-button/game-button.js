define(["vue", "./game-button.html"],
    function(vue, html) {
        "use strict";

        return {
            template: html,
            props: ['text', 'icon'],
            computed: {
                iconCssClass: function() {
                    return 'icon-' + this.icon;
                }
            }
        };
    }
);