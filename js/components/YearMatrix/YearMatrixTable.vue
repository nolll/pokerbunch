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
          :bunchId="slug"
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
import { computed } from 'vue';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import playerSorter from '@/PlayerSorter';
import archiveHelper from '@/ArchiveHelper';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';

const props = defineProps<{
  games: ArchiveCashgame[];
  currentYear: number;
  years: number[];
  slug: string;
}>();

const playersWithYearResults = computed((): CashgamePlayerYearlyResultCollection[] => {
  var matrixArray: CashgamePlayerYearlyResultCollection[] = [];
  if (!props.currentYear) return matrixArray;
  for (let i = 0; i < allYearsPlayers.value.length; i++) {
    const player = allYearsPlayers.value[i];
    const mostRecentGame = getMostRecentGame(player.gameResults);
    const buyinTime = mostRecentGame?.buyinTime;
    const yearOfMostRecentGame: number | null = !!buyinTime ? dayjs(buyinTime).year() : null;
    if (yearOfMostRecentGame === props.currentYear) {
      var playerYears = [];
      for (let k = 0; k < props.years.length; k++) {
        var year = props.years[k];
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
  return allYearsPlayers.value.length > 0;
});

const allYearsPlayers = computed((): CashgameListPlayerData[] => {
  return playerSorter.sort(archiveHelper.getPlayers(props.games), CashgamePlayerSortOrder.Winnings);
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
