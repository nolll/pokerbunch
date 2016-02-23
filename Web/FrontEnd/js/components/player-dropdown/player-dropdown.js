define(["vue", "text!components/player-dropdown/player-dropdown.html"],
    function(vue, html) {
        "use strict";

        return vue.component("player-dropdown", {
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