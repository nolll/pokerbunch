define(["vue", "./player-dropdown.html"],
    function(vue, html) {
        "use strict";

        return {
            template: html,
            props: ['playerId', 'players'],
            methods: {
                changePlayer: function () {
                    this.eventHub.$emit('change-player', this.selectedPlayerId);
                }
            },
            mounted: function() {
                this.selectedPlayerId = this.playerId;
            },
            data: function () {
                return {
                    selectedPlayerId: null
                }
            }
        };
    }
);