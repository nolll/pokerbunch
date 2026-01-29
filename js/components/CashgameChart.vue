<template>
  <div>
    <NewLineChart :chart-data="chartData2" :chart-options="chartOptions2" :ready="ready" />
  </div>
</template>

<script setup lang="ts">
import NewLineChart from './NewLineChart.vue';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import format from '@/format';
import { computed } from 'vue';
import archiveHelper from '@/ArchiveHelper';
import playerSorter from '@/PlayerSorter';

const props = defineProps<{
  games: ArchiveCashgame[];
  ready: boolean;
}>();

const chartData2 = computed(() => {
  return getChartData2(props.games, players.value);
});

const chartOptions2 = computed(() => {
  return {
    responsive: true,
    plugins: {
      legend: {
        display: true,
        position: 'bottom',
        labels: {
          boxWidth: 10,
          boxHeight: 10,
        },
      },
    },
  };
});

const players = computed(() => playerSorter.sort(archiveHelper.getPlayers(props.games)));

const getChartData2 = (games: ArchiveCashgame[], players: CashgameListPlayerData[]) => {
  return {
    labels: getLabels(games),
    datasets: getDatasets(games, players),
  };
};

const getLabels = (games: ArchiveCashgame[]): string[] => {
  var cols = [];
  cols.push('');
  for (var i = 0; i < games.length; i++) {
    var rgi = games.length - i - 1;
    cols.push(format.monthDay(games[rgi].date));
  }
  return cols;
};

const getDatasets = (games: ArchiveCashgame[], players: CashgameListPlayerData[]) => {
  var datasets = [];
  for (var i = 0; i < players.length; i++) {
    var player = players[i];
    var data = [];
    data.push(0);
    var accumulatedWinnings = 0;
    for (var j = 0; j < player.gameResults.length; j++) {
      var rgi = player.gameResults.length - j - 1;
      var game = player.gameResults[rgi];
      var val = accumulatedWinnings + game.winnings;
      accumulatedWinnings = val;
      data.push(val);
    }
    datasets.push({
      label: player.name,
      spanGaps: true,
      pointStyle: false,
      borderWidth: 1,
      data: data,
    });
  }
  return datasets;
};
</script>
