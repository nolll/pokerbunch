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
import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
import { computed } from 'vue';
import LineChart from './LineChart.vue';

const props = defineProps<{
  player: DetailedCashgamePlayer;
}>();

const chartOptions = computed((): ChartOptions => {
  var minmax = getMinMax();

  return {
    colors: ['#000'],
    vAxis: {
      minValue: 0,
      textPosition: 'none',
      gridlines: { color: 'transparent' },
      baselineColor: '#f6f4ef',
      viewWindowMode: 'explicit',
      viewWindow: {
        max: minmax.max,
        min: minmax.min,
      },
    },
    hAxis: { format: 'HH:mm', textPosition: 'none', gridlines: { color: 'transparent' } },
    pointSize: 0,
    legend: { position: 'none' },
    tooltip: { trigger: 'none' },
    enableInteractivity: false,
  };
});

const chartData = computed((): ChartData => {
  return getChartData();
});

const getChartData = (): ChartData => {
  return {
    colors: null,
    cols: [
      {
        type: ChartColumnType.DateTime,
        label: '',
        pattern: ChartColumnPattern.HoursAndMinutes,
      },
      {
        type: ChartColumnType.Number,
        label: '',
        pattern: null,
      },
    ],
    rows: getChartRows(),
    p: null,
  };
};

const getChartRows = (): ChartRow[] => {
  var rows = [];
  var buyin = 0;
  for (let i = 0; i < props.player.actions.length; i++) {
    let action = props.player.actions[i];
    if (action.added) {
      buyin += action.added;
    }

    rows.push(getChartRow(action.time, action.stack - buyin));
  }
  return rows;
};

const getMinMax = (): { min: number; max: number } => {
  var min = 0;
  var max = 0;
  var buyin = 0;
  for (let i = 0; i < props.player.actions.length; i++) {
    let action = props.player.actions[i];
    if (action.added) {
      buyin += action.added;
    }
    var result = action.stack - buyin;
    if (result > max) max = result;
    if (result < min) min = result;
  }

  return { min: min - 20, max: max + 20 };
};

const getChartRow = (time: Date, stack: number): ChartRow => ({
  c: [
    {
      v: new Date(time),
      f: null,
    },
    {
      v: stack,
      f: null,
    },
  ],
});
</script>
