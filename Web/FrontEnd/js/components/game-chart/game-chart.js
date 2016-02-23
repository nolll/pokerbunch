define(["vue", "linechart", "moment"],
    function (vue, lineChart, moment) {
        "use strict";

        var chart = null;

        return vue.component("game-chart", {
            template: '<div></div>',
            props: ['players'],
            watch: {
                'players': function (val) {
                    this.update();
                }
            },
            methods: {
                update: function () {
                    if (chart === null)
                        this.init();
                    var data = this.getGameChartData();
                    var options = { colors: this.getColors() };
                    chart.draw(data, options);
                },
                init: function() {
                    var config = {
                        pointSize: 0,
                        vAxis: { minValue: 0 },
                        hAxis: { format: 'HH:mm' },
                        legend: {
                            position: 'none'
                        }
                    };
                    chart = lineChart.init(this.$el, config);
                },
                getGameChartData: function () {
                    return {
                        cols: this.getColumns(),
                        rows: this.getRows(),
                        p: null
                    };
                },
                getColors: function() {
                    var i, p,
                        colors = [];
                    for (i = 0; i < this.players.length; i++) {
                        p = this.players[i];
                        colors.push(p.color);
                    }
                    return colors;
                },
                getColumns: function() {
                    var i, p,
                        cols = [{ type: "datetime", label: "Time", pattern: "HH:mm" }];
                    for (i = 0; i < this.players.length; i++) {
                        p = this.players[i];
                        cols.push({ type: "number", label: p.name, pattern: null });
                    }
                    return cols;
                },
                getRows : function() {
                    var i, j, p, r;
                    var rows = [];
                    for (i = 0; i < this.players.length; i++) {
                        p = this.players[i];
                        r = this.getPlayerResults(p);
                        for (j = 0; j < r.length; j++) {
                            rows.push(this.getRow(r[j], p.id));
                        }
                        if (!p.hasCashedOut) {
                            var currentResult = this.createPlayerResult(moment().utc(), r[r.length - 1].winnings);
                            rows.push(this.getRow(currentResult, p.id));
                        }
                    }
                    return rows;
                },
                getPlayerResults: function(player) {
                    var i,
                        c,
                        winnings,
                        addedMoney = 0,
                        results = [],
                        checkpoints = player.checkpoints;
                    for (i = 0; i < checkpoints.length; i++) {
                        c = checkpoints[i];
                        addedMoney += c.addedMoney;
                        winnings = c.stack - addedMoney;
                        results.push(this.createPlayerResult(c.time, winnings));
                    }
                    return results;
                },
                createPlayerResult: function(time, winnings) {
                    return { time: time, winnings: winnings };
                },
                getRow: function(result, playerId) {
                    var values = [{ v: moment(result.time).toDate(), f: null }];
                    for (var i = 0; i < this.players.length; i++) {
                        var val = null;
                        if (this.players[i].id === playerId) {
                            val = result.winnings + '';
                        }
                        values.push({ v: val, f: null });
                    }
                    return { c: values };
                }
            }
        });
    }
);