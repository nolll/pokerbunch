<template>
    <div v-if="ready">
        <LineChart :chart-data="chartData" :chart-options="chartOptions" />
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import moment from 'moment';
    import LineChart from './LineChart.vue';
    import { BunchMixin, GameArchiveMixin } from '@/mixins';
    import { ChartOptions } from '@/models/ChartOptions';
    import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
    import { ArchiveCashgame } from '@/models/ArchiveCashgame';
    import { ChartColumnType } from '@/models/ChartColumnType';
    import { ChartRowData } from '@/models/ChartRowData';
    import { ChartRow } from '@/models/ChartRow';

    @Component({
        components: {
            LineChart
        }
    })
    export default class CashgameChart extends Mixins(
        GameArchiveMixin,
        BunchMixin
    ) {
        chartOptions: ChartOptions = {
            pointSize: 0,
            legend: {
                position: 'none'
            }
        }

        get chartData() {
            if (!this.ready) {
                return null;
            }
            return getChartData(this.$_sortedGames, this.$_sortedPlayers);
        }

        get ready() {
            return this.$_bunchReady && this.$_gamesReady;
        }
    }

    function getChartData(games: ArchiveCashgame[], players: CashgameListPlayerData[]) {
        return {
            colors: getColors(players),
            cols: getCols(players),
            rows: getRows(games, players),
            p: null
        }
    }

    function getColors(players: CashgameListPlayerData[]) {
        var colors = [];
        for (var i = 0; i < players.length; i++) {
            colors.push('#000000');
        }
        return colors;
    }

    function getCols(players: CashgameListPlayerData[]) {
        var cols = [];
        cols.push(getCol(ChartColumnType.String, 'Date'));
        for (var i = 0; i < players.length; i++) {
            cols.push(getCol(ChartColumnType.Number, players[i].name));
        }
        return cols;
    }

    function getCol(type: ChartColumnType, label: string) {
        return {
            type: type,
            label: label,
            pattern: null
        };
    }

    function getRows(games: ArchiveCashgame[], players: CashgameListPlayerData[]) {
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
                var playerGame = player.gameResults[rgi];
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

    function getAccumulatedWinnings(players: CashgameListPlayerData[]) {
        var accumulated: Record<string, number> = {};
        for (var i = 0; i < players.length; i++) {
            accumulated[players[i].id] = 0;
        }
        return accumulated;
    }

    function getFirstRow(players: CashgameListPlayerData[]) {
        var points = [];
        points.push(getPoint(''));
        for (var i = 0; i < players.length; i++) {
            points.push(getPoint('0'));
        }
        return getRowObj(points);
    }

    function getPoint(val: Date | string | null): ChartRowData {
        return {
            v: val,
            f: null
        };
    }

    function getRowObj(points: ChartRowData[]): ChartRow {
        return {
            c: points
        };
    }
</script>
