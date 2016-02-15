define(["vue", "text!player-dropdown.html"],
    function(vue, html) {
        "use strict";

        return vue.extend({
            template: html,
            props: ['selectedPlayerId', 'players'],
            methods: {
                changePlayer: function () {
                    this.$dispatch('change-player', this.selectedPlayerId);
                }
            },
            ready: function() {
                var x = 0;
            }
        });
    }
);