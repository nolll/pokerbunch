<template>
    <div v-if="ready">
        <line-chart :chart-data="chartData" :chart-options="chartOptions" />
    </div>
</template>

<script>
    import moment from 'moment';
    import { mapState, mapGetters } from 'vuex';
    import { LineChart } from '.';
    import { BUNCH, GAME_ARCHIVE } from '@/store-names';

    export default {
        components: {
            LineChart
        },
        computed: {
            ...mapState(BUNCH, {
                bunchReady: state => state.bunchReady
            }),
            //...mapGetters(GAME_ARCHIVE, {
            //    gamesReady: getters => getters.gamesReady,
            //    startTime: getters => getters.startTime,
            //    sortedGames: getters => getters.sortedGames,
            //    sortedPlayers: getters => getters.sortedPlayers}
            //),
            ...mapGetters(GAME_ARCHIVE, [
                'gamesReady',
                'startTime',
                'sortedGames',
                'sortedPlayers'
            ]
            ),
            chartData() {
                if (!this.ready) {
                    return null;
                }
                return getChartData(this.sortedGames, this.sortedPlayers);
            },
            ready() {
                console.log(BUNCH);
                console.log(GAME_ARCHIVE);
                return this.bunchReady && this.gamesReady;
            }
        },
        data: function () {
            return {
                chartOptions: {
                    pointSize: 0,
                    legend: {
                        position: 'none'
                    }
                }
            }
        }
    };

    function getChartData(games, players) {
        return {
            colors: getColors(players),
            cols: getCols(players),
            rows: getRows(games, players),
            p: null
        }
    }

    function getColors(players) {
        var colors = [];
        for (var i = 0; i < players.length; i++) {
            colors.push('#000000');
        }
        return colors;
    }

    function getCols(players) {
        var cols = [];
        cols.push(getCol('string', 'Date'));
        for (var i = 0; i < players.length; i++) {
            cols.push(getCol('number', players[i].name));
        }
        return cols;
    }

    function getCol(type, label) {
        return {
            type: type,
            label: label,
            pattern: null
        };
    }

    function getRows(games, players) {
        var rows = [];
        rows.push(getFirstRow(players));
        var gameCount = games.length;
        var accumulatedWinnings = getAccumulatedWinnings(players);
        for (var gi = 0; gi < gameCount; gi++) {
            var rgi = gameCount - gi - 1;
            var fillEndValues = rgi === 0;
            var game = games[rgi];
            var points = [];
            var formattedDate = moment(game.date).format('MMM D');
            points.push(getPoint(formattedDate));
            for (var pi = 0; pi < players.length; pi++) {
                var player = players[pi];
                var playerGame = player.games[rgi];
                var chartVal = null;
                if (playerGame) {
                    var val = accumulatedWinnings[player.id] + playerGame.winnings;
                    accumulatedWinnings[player.id] = val;
                    chartVal = val.toString();
                } else if (fillEndValues) {
                    chartVal = accumulatedWinnings[player.id].toString();
                }
                points.push(getPoint(chartVal));
            }
            rows.push(getRowObj(points))
        }
        return rows;
    }

    function getAccumulatedWinnings(players) {
        var accumulated = {};
        for (var i = 0; i < players.length; i++) {
            accumulated[players[i].id] = 0;
        }
        return accumulated;
    }

    function getFirstRow(players) {
        var points = [];
        points.push(getPoint(''));
        for (var i = 0; i < players.length; i++) {
            points.push(getPoint('0'));
        }
        return getRowObj(points);
    }

    function getPoint(val) {
        return {
            v: val,
            f: null
        };
    }

    function getRowObj(points) {
        return {
            c: points
        };
    }
</script>

<style>
</style>
