<template>
  <div>
    <LineChart :chart-data="chartData" :chart-options="chartOptions" />
  </div>

  <div>
    <NewLineChart :chart-data="chartData2" :chart-options="chartOptions2" :ready="ready" />
  </div>
</template>

<script setup lang="ts">
import LineChart from './LineChart.vue';
import NewLineChart from './NewLineChart.vue';
import { ChartOptions } from '@/models/ChartOptions';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { ChartColumnType } from '@/models/ChartColumnType';
import { ChartRowData } from '@/models/ChartRowData';
import { ChartRow } from '@/models/ChartRow';
import format from '@/format';
import { computed } from 'vue';
import archiveHelper from '@/ArchiveHelper';
import playerSorter from '@/PlayerSorter';
import { NewChartDataset } from '@/models/ChartData';

const props = defineProps<{
  games: ArchiveCashgame[];
  ready: Boolean;
}>();

const chartOptions: ChartOptions = {
  pointSize: 0,
  legend: {
    position: 'none',
  },
};

const chartData = computed(() => {
  return getChartData(props.games, players.value);
});

const chartData2 = computed(() => {
  return getChartData2(props.games, players.value);
});

const chartOptions2 = computed(() => {
  return {
    responsive: true,
  };
});

const players = computed(() => playerSorter.sort(archiveHelper.getPlayers(props.games)));

const getChartData = (games: ArchiveCashgame[], players: CashgameListPlayerData[]) => {
  return {
    colors: getColors(players),
    cols: getCols(players),
    rows: getRows(games, players),
    p: null,
  };
};

const getChartData2 = (games: ArchiveCashgame[], players: CashgameListPlayerData[]) => {
  return {
    //colors: getColors(players),
    labels: getLabels(games),
    datasets: getDatasets(games, players),
  };
};

const getColors = (players: CashgameListPlayerData[]) => {
  var colors = [];
  for (var i = 0; i < players.length; i++) {
    colors.push('#000000');
  }
  return colors;
};

const getCols = (players: CashgameListPlayerData[]) => {
  var cols = [];
  cols.push(getCol(ChartColumnType.String, 'Date'));
  for (var i = 0; i < players.length; i++) {
    cols.push(getCol(ChartColumnType.Number, players[i].name));
  }
  return cols;
};

const getLabels = (games: ArchiveCashgame[]): string[] => {
  var cols = [];
  cols.push('');
  for (var i = 0; i < games.length; i++) {
    cols.push(format.monthDay(games[i].date));
  }
  return cols;
};

const getCol = (type: ChartColumnType, label: string) => {
  return {
    type: type,
    label: label,
    pattern: null,
  };
};

const getRows = (games: ArchiveCashgame[], players: CashgameListPlayerData[]) => {
  var rows = [];
  rows.push(getFirstRow(players));
  var gameCount = games.length;
  var accumulatedWinnings = getAccumulatedWinnings(players);
  for (var gi = 0; gi < gameCount; gi++) {
    var rgi = gameCount - gi - 1;
    var fillEndValues = rgi === 0;
    var game = games[rgi];
    var points = [];
    var formattedDate = format.monthDay(game.date);
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
    rows.push(getRowObj(points));
  }
  return rows;
};

const getDatasets = (games: ArchiveCashgame[], players: CashgameListPlayerData[]) => {
  var datasets = [];
  var accumulatedWinnings = getAccumulatedWinnings(players);
  for (var i = 0; i < players.length; i++) {
    var player = players[i];
    var data = [];
    data.push(0);
    console.log(player.gameResults);
    for (var j = 0; j < player.gameResults.length; j++) {
      var game = player.gameResults[j];
      var val = accumulatedWinnings[player.id] + game.winnings;
      data.push(val);
    }
    var dataset = {
      label: player.name,
      data: data,
    };
    datasets.push(dataset);
  }
  console.log(datasets);
  return datasets;
};

const getAccumulatedWinnings = (players: CashgameListPlayerData[]) => {
  var accumulated: Record<string, number> = {};
  for (var i = 0; i < players.length; i++) {
    accumulated[players[i].id] = 0;
  }
  return accumulated;
};

const getFirstRow = (players: CashgameListPlayerData[]) => {
  var points = [];
  points.push(getPoint(''));
  for (var i = 0; i < players.length; i++) {
    points.push(getPoint('0'));
  }
  return getRowObj(points);
};

const getPoint = (val: Date | string | null): ChartRowData => {
  return {
    v: val,
    f: null,
  };
};

const getRowObj = (points: ChartRowData[]): ChartRow => {
  return {
    c: points,
  };
};

const getRowObj2 = (points: (number | null)[]): NewChartDataset => {
  return {
    data: points,
  };
};
</script>
