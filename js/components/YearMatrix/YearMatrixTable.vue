<template>
  <div class="matrix">
    <TableList>
      <thead>
        <tr>
          <TableListColumnHeader @click="toggleAll"><i :class="toggleIcon"></i></TableListColumnHeader>
          <TableListColumnHeader>Player</TableListColumnHeader>
          <TableListColumnHeader>Winnings</TableListColumnHeader>
          <YearMatrixColumn v-for="year in years" :year="year" :key="year" />
        </tr>
      </thead>
      <tbody>
        <YearMatrixRow
          v-for="player in playersWithYearResults"
          :player="player"
          :key="player.id"
          :bunchId="bunchId"
          :localization="localization"
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
import { TableList, TableListColumnHeader } from '@/components/Common/TableList';
import { computed, ref } from 'vue';
import { BunchResponse } from '@/response/BunchResponse';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import ArchiveHelper from '@/ArchiveHelper';
import { Localization } from '@/models/Localization';

const props = defineProps<{
  bunch: BunchResponse;
  games: ArchiveCashgame[];
  localization: Localization;
}>();

const showAll = ref(false);

const bunchId = computed(() => {
  return props.bunch.id;
});

const years = computed(() => {
  return ArchiveHelper.getYears(props.games);
});

const currentYear = computed(() => {
  return ArchiveHelper.getCurrentYear(props.games);
});

const players = computed(() => {
  return ArchiveHelper.getPlayers(props.games);
});

const playersWithYearResults = computed((): CashgamePlayerYearlyResultCollection[] => {
  var matrixArray: CashgamePlayerYearlyResultCollection[] = [];
  if (!currentYear.value) return matrixArray;
  for (let i = 0; i < players.value.length; i++) {
    const player = players.value[i];
    const mostRecentGame = getMostRecentGame(player.gameResults);
    const buyinTime = mostRecentGame?.buyinTime;
    const yearOfMostRecentGame: number | null = !!buyinTime ? dayjs(buyinTime).year() : null;
    if (showAll.value || yearOfMostRecentGame === currentYear.value) {
      var playerYears = [];
      for (let k = 0; k < years.value.length; k++) {
        var year = years.value[k];
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
        rank: i + 1,
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

const toggleIcon = computed(() => {
  return showAll.value ? 'pi pi-chevron-up' : 'pi pi-chevron-down';
});

const toggleAll = () => {
  showAll.value = !showAll.value;
};
</script>
