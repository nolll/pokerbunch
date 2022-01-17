<template>
  <div>
    <LineChart :chart-data="chartData" :chart-options="chartOptions" />
  </div>
</template>

<script setup lang="ts">
import dayjs from 'dayjs';
import utc from 'dayjs/plugin/utc';
import LineChart from '@/components/LineChart.vue';
import { ChartData } from '@/models/ChartData';
import { ChartRow } from '@/models/ChartRow';
import { ChartColumn } from '@/models/ChartColumn';
import { ChartColumnType } from '@/models/ChartColumnType';
import { ChartColumnPattern } from '@/models/ChartColumnPattern';
import { ChartRowData } from '@/models/ChartRowData';
import { ChartOptions } from '@/models/ChartOptions';
import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
import { nextTick, ref, watch } from 'vue';

interface ChartPlayerResult {
  time: Date;
  winnings: number;
}

dayjs.extend(utc);

const props = defineProps<{
  players: DetailedCashgamePlayer[];
}>();

const chartData = ref<ChartData | null>(null);
const chartOptions = ref<ChartOptions>({
  pointSize: 0,
  vAxis: { minValue: 0 },
  hAxis: { format: 'HH:mm' },
  legend: {
    position: 'none',
  },
  colors: [],
});

const drawChart = () => {
  chartData.value = getGameChartData();
  chartOptions.value = { ...chartOptions.value, colors: getColors() };
};

const getGameChartData = (): ChartData => {
  return {
    colors: null,
    cols: getColumns(),
    rows: getRows(),
    p: null,
  };
};

const getColors = () => {
  const colors = [];
  for (const p of props.players) {
    colors.push(p.color);
  }
  return colors;
};

const getColumns = (): ChartColumn[] => {
  const cols: ChartColumn[] = [{ type: ChartColumnType.DateTime, label: 'Time', pattern: ChartColumnPattern.HoursAndMinutes }];
  for (const p of props.players) {
    cols.push({ type: ChartColumnType.Number, label: p.name, pattern: null });
  }
  return cols;
};

const getRows = (): ChartRow[] => {
  var rows = [];
  for (const player of props.players) {
    if (player.actions.length) {
      const results = getPlayerResults(player);
      for (let j = 0; j < results.length; j++) {
        rows.push(getRow(results[j], player.id));
      }
      if (!player.hasCashedOut()) {
        var currentResult = createPlayerResult(dayjs.utc().toDate(), results[results.length - 1].winnings);
        rows.push(getRow(currentResult, player.id));
      }
    }
  }
  return rows;
};

const getPlayerResults = (player: DetailedCashgamePlayer) => {
  let added = 0;
  const results = [];
  const actions = player.actions;
  for (let i = 0; i < actions.length; i++) {
    const a = actions[i];
    added += a.added || 0;
    const winnings = a.stack - added;
    results.push(createPlayerResult(a.time, winnings));
  }
  return results;
};

const createPlayerResult = (time: Date, winnings: number): ChartPlayerResult => {
  return {
    time: time,
    winnings: winnings,
  };
};

const getRow = (result: ChartPlayerResult, playerId: string) => {
  const values: ChartRowData[] = [{ v: result.time, f: null }];
  for (var i = 0; i < props.players.length; i++) {
    var val = null;
    if (props.players[i].id === playerId) {
      val = result.winnings + '';
    }
    values.push({ v: val, f: null });
  }
  return { c: values };
};

const mounted = async () => {
  await nextTick();
  if (props.players.length > 0) {
    drawChart();
  }
};

watch(
  () => props.players,
  () => {
    if (!!props.players) drawChart();
  }
);
</script>
