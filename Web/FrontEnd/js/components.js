define([
    "vue",
    "standings",
    "components/player-dropdown",
    "components/game-button",
    "components/player-table",
    "components/player-row",
    "components/report-form",
    "components/buyin-form",
    "components/cashout-form",
    "components/endgame-form",
    "components/spinner"],
function (vue, standings, playerDropdown, gameButton, playerTable, playerRow, reportForm, buyinForm, cashoutForm, endgameForm, spinner) {

        "use strict";

        function init() {
            //vue.config.debug = true;
            vue.component('player-dropdown', playerDropdown);
            vue.component('game-button', gameButton);
            vue.component('player-table', playerTable);
            vue.component('player-row', playerRow);
            vue.component('report-form', reportForm);
            vue.component('cashout-form', cashoutForm);
            vue.component('endgame-form', endgameForm);
            vue.component('buyin-form', buyinForm);
            vue.component('spinner', spinner);

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