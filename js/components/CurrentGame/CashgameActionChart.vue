<template>
  <div>
    <OldLineChart :chart-data="chartData" :chart-options="chartOptions" />
  </div>

  <div>
    <LineChart :chart-data="chartData2" :chart-options="chartOptions2" :ready="true" />
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
import { ChartData as ChartData2, ChartOptions as ChartOptions2, Point } from 'chart.js';
import OldLineChart from '@/components/OldLineChart.vue';
import LineChart from '@/components/LineChart.vue';

const props = defineProps<{
  player: DetailedCashgamePlayer;
}>();

const chartOptions: ChartOptions = {
  colors: ['#000', '#ABA493'],
  series: { 1: { type: 'area' } },
  vAxis: { minValue: 0, baselineColor: 'transparent' },
  hAxis: { format: 'HH:mm' },
  pointSize: 0,
};

const chartOptions2 = computed((): ChartOptions2<'line'> => {
  return {
    responsive: true,
    maintainAspectRatio: true,
    aspectRatio: 3,
    plugins: {
      legend: {
        display: false,
      },
    },
    scales: {
      x: {
        type: 'time',
        time: {
          unit: 'minute',
          displayFormats: {
            minute: 'HH:mm',
          },
        },
      },
      y: {
        beginAtZero: true,
      },
    },
  };
});

const chartData2 = computed((): ChartData2<'line'> => {
  return {
    //labels: props.player.actions.map((a) => a.time),
    datasets: [
      {
        label: 'Buyin',
        backgroundColor: 'rgba(100, 100, 100, 0.35)',
        borderColor: 'rgba(100, 100, 100, 0.35)',
        fill: 'origin',
        pointRadius: 0,
        borderWidth: 1,
        data: props.player.actions.map((a) => {
          return {
            x: a.time.getTime(),
            y: 200,
          };
        }),
      },
      {
        label: 'Stack',
        backgroundColor: '#000000',
        borderColor: '#000000',
        pointRadius: 0,
        borderWidth: 1,
        data: props.player.actions.map((a) => {
          return {
            x: a.time.getTime(),
            y: a.stack,
          };
        }),
      },
    ],
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
