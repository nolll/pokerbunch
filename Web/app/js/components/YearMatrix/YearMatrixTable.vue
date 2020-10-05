<template>
    <div class="matrix" v-if="ready">
        <TableList>
            <thead>
                <tr>
                    <TableListColumnHeader />
                    <TableListColumnHeader>Player</TableListColumnHeader>
                    <TableListColumnHeader>Winnings</TableListColumnHeader>
                    <YearMatrixColumn v-for="year in years" :year="year" :key="year" />
                </tr>
            </thead>
            <tbody>
                <YearMatrixRow v-for="(player, index) in playersWithYearResults" :player="player" :index="index" :key="player.id" />
            </tbody>
        </TableList>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import YearMatrixColumn from './YearMatrixColumn.vue';
    import YearMatrixRow from './YearMatrixRow.vue';
    import { BunchMixin, GameArchiveMixin } from '@/mixins';
    import { CashgamePlayerData } from '@/models/CashgamePlayerData';
    import { CashgamePlayerYearlyResultCollection } from '@/models/CashgamePlayerYearlyResultCollection';
    import dayjs from 'dayjs';
    import TableList from '@/components/Common/TableList/TableList.vue';
    import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';

    @Component({
        components: {
            YearMatrixColumn,
            YearMatrixRow,
            TableList,
            TableListColumnHeader
        }
    })
    export default class YearMatrixTable extends Mixins(
        BunchMixin,
        GameArchiveMixin
    ) {
        get years(){
            return this.$_years;
        }

        get playersWithYearResults(): CashgamePlayerYearlyResultCollection[] {
            var matrixArray: CashgamePlayerYearlyResultCollection[] = [];
            if(!this.$_currentYear)
                return matrixArray;
            for (let i = 0; i < this.$_allYearsPlayers.length; i++) {
                const player = this.$_allYearsPlayers[i];
                const mostRecentGame = getMostRecentGame(player.gameResults);
                const buyinTime = mostRecentGame?.buyinTime;
                const yearOfMostRecentGame: number | null = !!buyinTime ? dayjs(buyinTime).year() : null;
                if (yearOfMostRecentGame === this.$_currentYear) {
                    var playerYears = [];
                    for (let k = 0; k < this.$_years.length; k++) {
                        var year = this.$_years[k]
                        var yearGames = getGamesForYear(player.gameResults, year);
                        var playerYear = {
                            winnings: 0,
                            playedThisYear: yearGames.length > 0
                        }
                        for (let j = 0; j < yearGames.length; j++) {
                            var game = yearGames[j];
                            playerYear.winnings += game.winnings;
                        }
                        playerYears.push(playerYear);
                    }
                    var matrixPlayer = {
                        id: player.id,
                        name: player.name,
                        winnings: player.winnings,
                        years: playerYears
                    };
                    matrixArray.push(matrixPlayer);
                }
            }
            return matrixArray;
        }

        get ready() {
            return this.$_bunchReady && this.$_allYearsPlayers.length > 0;
        }
    }

    function getMostRecentGame(games: CashgamePlayerData[]) {
        for (let i = 0; i < games.length; i++) {
            if (games[i] && games[i].playedThisGame) {
                return games[i];
            }
        }
        return null;
    }

    function getGamesForYear(games: CashgamePlayerData[], year: number) {
        var yearGames = [];
        for (let i = 0; i < games.length; i++) {
            var game = games[i];
            if (!!game && game.playedThisGame && !!game.buyinTime && dayjs(game.buyinTime).year() === year) {
                yearGames.push(game);
            }
        }
        return yearGames;
    }
</script>
