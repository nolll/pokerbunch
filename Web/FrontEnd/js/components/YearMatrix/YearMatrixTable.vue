<template>
    <div class="matrix" v-if="ready">
        <table class="table-list">
            <thead>
                <tr>
                    <th class="table-list__column-header"></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Player</span></th>
                    <th class="table-list__column-header"><span class="table-list__column-header__content">Winnings</span></th>
                    <th is="year-matrix-column" v-for="year in years" :year="year"></th>
                </tr>
            </thead>
            <tbody>
                <tr is="year-matrix-row" v-for="(player, index) in playersWithYearResults" :player="player" :index="index"></tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import { mapState, mapGetters } from 'vuex';
    import { YearMatrixColumn, YearMatrixRow } from '.';
    import { BUNCH, GAME_ARCHIVE } from '@/store-names';

    export default {
        components: {
            YearMatrixColumn,
            YearMatrixRow
        },
        computed: {
            ...mapState(GAME_ARCHIVE, [
                'games'
            ]),
            ...mapState(BUNCH, [
                'bunchReady'
            ]),
            ...mapGetters(GAME_ARCHIVE, [
                'currentYearPlayers',
                'allYearsPlayers',
                'years',
                'currentYear'
            ]),
            playersWithYearResults() {
                var matrixArray = [];
                for (let i = 0; i < this.allYearsPlayers.length; i++) {
                    var player = this.allYearsPlayers[i];
                    var mostRecentGame = getMostRecentGame(player.games);
                    var yearOfMostRecentGame = mostRecentGame.buyinTime.year();
                    if (yearOfMostRecentGame === this.currentYear) {
                        var playerYears = [];
                        for (let k = 0; k < this.years.length; k++) {
                            var yearGames = getGamesForYear(player.games, this.years[k]);
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
            },
            ready() {
                return this.bunchReady && this.allYearsPlayers.length > 0;
            }
        }
    };

    function getMostRecentGame(games) {
        for (let i = 0; i < games.length; i++) {
            if (games[i]) {
                return games[i];
            }
        }
        return null;
    }

    function getGamesForYear(games, year) {
        var yearGames = [];
        for (let i = 0; i < games.length; i++) {
            var game = games[i];
            if (game && game.buyinTime.year() === year) {
                yearGames.push(game);
            }
        }
        return yearGames;
    }
</script>

<style>

</style>
