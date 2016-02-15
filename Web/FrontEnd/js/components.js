define(["vue", "standings", "player-dropdown", "game-button", "player-table", "player-row"],
    function (vue, standings, playerDropdown, gameButton, playerTable, playerRow) {

        "use strict";

        function init() {
            //vue.component('standings', standings);
            //vue.config.debug = true;
            vue.component('player-dropdown', playerDropdown);
            vue.component('game-button', gameButton);
            vue.component('player-table', playerTable);
            vue.component('player-row', playerRow);

            vue.filter('currency', function (value, format) {
                var f = format !== undefined ? format : '${0}';
                return f.replace('{0}', value);
            });

            vue.filter('result', function (value) {
                if(value > 0)
                    return "+" + value;
                return value;
            });
        }

        return {
            init: init
        };
    }
);