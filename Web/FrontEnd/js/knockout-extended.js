define([
    'knockout-raw', 'linechart2'
], function (ko, lineChart) {
    ko.bindingHandlers.gameChart = {
        init: function(element) {
            var config = {
                pointSize: 0,
                vAxis: { minValue: 0 },
                hAxis: { format: 'HH:mm' }
            };
            var chart = lineChart.init(element, config);
            ko.utils.domData.set(element, 'chart', chart);
        },
        update: function (element, valueAccessor) {
            var players = valueAccessor();
            var data = getGameChartData(players());
            var chart = ko.utils.domData.get(element, 'chart');
            chart.draw(data);
        }
    };

    function getGameChartData(players) {
        var data = {
            cols: getColumns(players),
            rows: getRows(players),
            p: null
        };
        return data;
    }

    function getColumns(players) {
        var i, p,
            cols = [{ type: "datetime", label: "Time", pattern: "HH:mm" }];
        for(i = 0; i < players.length; i++) {
            p = players[i];
            cols.push({ type: "number", label: p.name, pattern: null });
        }
        return cols;
    }

    function getRows(players) {
        var i, j, p, r;
        var rows = [];
        for(i = 0; i < players.length; i++) {
            p = players[i];
            r = getPlayerResults(p);
            for(j = 0; j < r.length; j++) {
                rows.push(getRow(players, r[j], p.id));
            }
        }
        return rows;
    }

    function getPlayerResults(player) {
        var i,
            c,
            winnings = 0,
            addedMoney = 0,
            results = [],
            checkpoints = player.checkpoints();
        for (i = 0; i < checkpoints.length; i++) {
            c = checkpoints[i];
            addedMoney += c.addedMoney;
            winnings = c.stack - addedMoney;
            results.push({time: c.time, winnings: winnings});
        }
        return results;
    }

    function getRow(players, result, playerId) {
        var values = [{ v: result.time.toDate(), f: null }];
        for(var i = 0; i < players.length; i++) {
            var val = null;
            if (players[i].id == playerId)
            {
                val = result.winnings + '';
            }
            values.push({ v: val, f: null });
        }
        return { c: values };
    }

    return ko;
});