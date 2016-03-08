define([
    "vue",
    "components/game-control/game-control",
    "components/dashboard/dashboard",
    "components/player-dropdown/player-dropdown",
    "components/game-button/game-button",
    "components/player-table/player-table",
    "components/player-row/player-row",
    "components/report-form/report-form",
    "components/buyin-form/buyin-form",
    "components/cashout-form/cashout-form",
    "components/endgame-form/endgame-form",
    "components/spinner/spinner",
    "components/game-chart/game-chart",
    "components/cancel-button/cancel-button"],
function (vue, gameControl, dashboard, playerDropdown, gameButton, playerTable, playerRow, reportForm, buyinForm, cashoutForm, endgameForm, spinner, gameChart, cancelButton) {

        "use strict";

        function init() {
            vue.config.debug = true;

            vue.component('game-control', gameControl);
            vue.component('dashboard', dashboard);
            vue.component('player-dropdown', playerDropdown);
            vue.component('game-button', gameButton);
            vue.component('player-table', playerTable);
            vue.component('player-row', playerRow);
            vue.component('report-form', reportForm);
            vue.component('cashout-form', cashoutForm);
            vue.component('endgame-form', endgameForm);
            vue.component('buyin-form', buyinForm);
            vue.component('spinner', spinner);
            vue.component('game-chart', gameChart);
            vue.component('cancel-button', cancelButton);

            vue.filter('currency', function (value, format) {
                var f = format !== undefined ? format : '${0}';
                return f.replace('{0}', value);
            });

            vue.filter('result', function (value) {
                if (value > 0)
                    return "+" + value;
                return value;
            });

            new vue({
                el: "body"
            });
        }

        return {
            init: init
        };
    }
);