<template>
  <div>
    <LineChart :chart-data="chartData" :chart-options="chartOptions" />
  </div>
</template>

<script setup lang="ts">
import { ChartColumnPattern } from '@/models/ChartColumnPattern';
import { ChartColumnType } from '@/models/ChartColumnType';
import { ChartData } from '@/models/ChartData';
import { ChartOptions } from '@/models/ChartOptions';
import { ChartRow } from '@/models/ChartRow';
import { DetailedCashgameResponsePlayer } from '@/response/DetailedCashgameResponsePlayer';
import { computed } from 'vue';
import LineChart from './LineChart.vue';

const props = defineProps<{
  player: DetailedCashgameResponsePlayer;
}>();

const chartOptions: ChartOptions = {
  colors: ['#000', '#ABA493'],
  series: { 1: { type: 'area' } },
  vAxis: { minValue: 0 },
  hAxis: { format: 'HH:mm' },
  pointSize: 0,
};

const chartData = computed((): ChartData => {
  return getChartData();
});

const getChartData = (): ChartData => {
  return {
    colors: null,
    cols: [
      {
        type: ChartColumnType.DateTime,
        label: 'Time',
        pattern: ChartColumnPattern.HoursAndMinutes,
      },
      {
        type: ChartColumnType.Number,
        label: 'Stack',
        pattern: null,
      },
      {
        type: ChartColumnType.Number,
        label: 'Buyin',
        pattern: null,
      },
    ],
    rows: getChartRows(),
    p: null,
  };
};

const getChartRows = (): ChartRow[] => {
  var buyin = 0;
  var rows = [];
  for (let i = 0; i < props.player.actions.length; i++) {
    let action = props.player.actions[i];
    if (action.added) {
      let stackBeforeBuyin = action.stack - action.added;
      rows.push(getChartRow(action.time, stackBeforeBuyin, buyin));
      buyin += action.added;
    }
    rows.push(getChartRow(action.time, action.stack, buyin));
  }
  return rows;
};

const getChartRow = (time: Date, stack: number, buyin: number): ChartRow => {
  return {
    c: [
      {
        v: new Date(time),
        f: null,
      },
      {
        v: stack,
        f: null,
      },
      {
        v: buyin,
        f: null,
      },
    ],
  };
};
</script>
