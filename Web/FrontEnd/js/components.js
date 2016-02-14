define(["vue", "standings", "player-dropdown", "game-button", "player-table"],
    function (vue, standings, playerDropdown, gameButton, playerTable) {

        "use strict";

        function init() {
            //vue.component('standings', standings);
            //vue.config.debug = true;
            vue.component('player-dropdown', playerDropdown);
            vue.component('game-button', gameButton);
            vue.component('player-table', playerTable);
        }

        return {
            init: init
        };
    }
);