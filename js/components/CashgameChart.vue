<template>
  <div style="position: relative">
    <LineChart :chart-data="chartData" :chart-options="chartOptions" :ready="ready" />
  </div>
</template>

<script setup lang="ts">
import LineChart from './LineChart.vue';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import format from '@/format';
import { computed } from 'vue';
import archiveHelper from '@/ArchiveHelper';
import playerSorter from '@/PlayerSorter';
import { ChartData, ChartDataset, ChartOptions } from 'chart.js';
import { getColor } from '@/colors';

const props = defineProps<{
  games: ArchiveCashgame[];
  ready: boolean;
}>();

const chartData = computed<ChartData<'line'>>(() => getChartData(props.games, players.value));

const chartOptions = computed<ChartOptions<'line'>>(() => ({
  responsive: true,
  aspectRatio: 4 / 3,
  maintainAspectRatio: true,
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
}));

const players = computed(() => playerSorter.sort(archiveHelper.getPlayers(props.games)));

const getChartData = (games: ArchiveCashgame[], players: CashgameListPlayerData[]) => ({
  labels: getLabels(games),
  datasets: getDatasets(players),
});

const getLabels = (games: ArchiveCashgame[]): string[] => {
  var cols = [];
  cols.push('');
  for (var i = 0; i < games.length; i++) {
    var rgi = games.length - i - 1;
    cols.push(format.monthDay(games[rgi].date));
  }
  return cols;
};

const getDatasets = (players: CashgameListPlayerData[]) => {
  var datasets: ChartDataset<'line'>[] = [];
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

    var color = playerColors.value[player.id];

    datasets.push({
      label: player.name,
      spanGaps: true,
      pointStyle: false,
      borderWidth: 3,
      borderColor: color,
      backgroundColor: color,
      data: data,
    });
  }
  return datasets;
};

const playerColors = computed(() => {
  const colors = {} as Record<string, string>;
  const sorted = [...players.value].sort((a, b) => a.name.localeCompare(b.name));
  for (let i = 0; i < sorted.length; i++) {
    colors[sorted[i].id] = getColor(i);
  }
  return colors;
});
</script>
