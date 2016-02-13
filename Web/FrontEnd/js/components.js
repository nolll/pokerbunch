define(["vue", "standings", "player-dropdown", "game-button"],
    function (vue, standings, playerDropdown, gameButton) {

        "use strict";

        function init() {
            //vue.component('standings', standings);
            vue.component('player-dropdown', playerDropdown);
            vue.component('game-button', gameButton);
        }

        return {
            init: init
        };
    }
);