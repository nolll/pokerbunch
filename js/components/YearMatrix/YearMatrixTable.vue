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
        <YearMatrixRow
          v-for="(player, index) in playersWithYearResults"
          :player="player"
          :index="index"
          :key="player.id"
          :bunchId="bunchId"
        />
      </tbody>
    </TableList>
  </div>
</template>

<script setup lang="ts">
import YearMatrixColumn from './YearMatrixColumn.vue';
import YearMatrixRow from './YearMatrixRow.vue';
import { CashgamePlayerData } from '@/models/CashgamePlayerData';
import { CashgamePlayerYearlyResultCollection } from '@/models/CashgamePlayerYearlyResultCollection';
import dayjs from 'dayjs';
import TableList from '@/components/Common/TableList/TableList.vue';
import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';
import useBunches from '@/composables/useBunches';
import useGameArchive from '@/composables/old/useGameArchive';
import { computed } from 'vue';

const bunches = useBunches();
const gameArchive = useGameArchive();

const bunchId = computed(() => {
  return bunches.slug.value;
});

const years = computed(() => {
  return gameArchive.years.value;
});

const playersWithYearResults = computed((): CashgamePlayerYearlyResultCollection[] => {
  var matrixArray: CashgamePlayerYearlyResultCollection[] = [];
  if (!gameArchive.currentYear.value) return matrixArray;
  for (let i = 0; i < gameArchive.allYearsPlayers.value.length; i++) {
    const player = gameArchive.allYearsPlayers.value[i];
    const mostRecentGame = getMostRecentGame(player.gameResults);
    const buyinTime = mostRecentGame?.buyinTime;
    const yearOfMostRecentGame: number | null = !!buyinTime ? dayjs(buyinTime).year() : null;
    if (yearOfMostRecentGame === gameArchive.currentYear.value) {
      var playerYears = [];
      for (let k = 0; k < gameArchive.years.value.length; k++) {
        var year = gameArchive.years.value[k];
        var yearGames = getGamesForYear(player.gameResults, year);
        var playerYear = {
          year: year,
          winnings: 0,
          playedThisYear: yearGames.length > 0,
        };
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
        years: playerYears,
      };
      matrixArray.push(matrixPlayer);
    }
  }
  return matrixArray;
});

const ready = computed(() => {
  return bunches.bunchReady.value && gameArchive.allYearsPlayers.value.length > 0;
});

const getMostRecentGame = (games: CashgamePlayerData[]) => {
  for (let i = 0; i < games.length; i++) {
    if (games[i] && games[i].playedThisGame) {
      return games[i];
    }
  }
  return null;
};

const getGamesForYear = (games: CashgamePlayerData[], year: number) => {
  var yearGames = [];
  for (let i = 0; i < games.length; i++) {
    var game = games[i];
    if (!!game && game.playedThisGame && !!game.buyinTime && dayjs(game.buyinTime).year() === year) {
      yearGames.push(game);
    }
  }
  return yearGames;
};
</script>
