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
    "components/top-list-table/top-list-table",
    "components/top-list-column/top-list-column",
    "components/top-list-row/top-list-row",
    "components/game-list-table/game-list-table",
    "components/game-list-column/game-list-column",
    "components/game-list-row/game-list-row"
],
function (vue, gameControl, dashboard, playerDropdown, gameButton, playerTable, playerRow, reportForm, buyinForm, cashoutForm, endgameForm, spinner, gameChart, topListTable, topListColumn, topListRow, gameListTable, gameListColumn, gameListRow) {

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
            vue.component('top-list-table', topListTable);
            vue.component('top-list-column', topListColumn);
            vue.component('top-list-row', topListRow);
            vue.component('game-list-table', gameListTable);
            vue.component('game-list-column', gameListColumn);
            vue.component('game-list-row', gameListRow);

            vue.filter('currency', function (value, format, separator) {
                return formatCurrency(value, format, separator)
            });

            vue.filter('result', function (value, format, separator) {
                var absValue = Math.abs(value);
                var currencyValue = formatCurrency(absValue, format, separator);
                if (value > 0)
                    return "+" + currencyValue;
                return "-" + currencyValue;
            });

            vue.filter('winrate', function (value, format, separator) {
                var currencyValue = formatCurrency(value, format, separator);
                return currencyValue + "/h";
            });

            vue.filter('time', function (minutes) {
                var h = Math.floor(minutes / 60);
                var m = minutes % 60;
                if (h > 0 && m > 0)
                    return h + "h " + m + "m";
                if (h > 0)
                    return h + "h";
                return m + "m";
            });

            function formatCurrency(value, format, separator) {
                var f = format !== undefined ? format : '${0}';
                var s = separator !== undefined ? separator : ",";
                var v = value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, s);
                return f.replace('{0}', v);
            }

            new vue({
                el: "body"
            });
        }

        return {
            init: init
        };
    }
);