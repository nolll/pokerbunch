<template>
  <div style="position: relative; width: 180px; height: 60px">
    <NewLineChart :chart-data="chartData" :chart-options="chartOptions" :ready="true" />
  </div>
</template>

<script setup lang="ts">
import NewLineChart from './NewLineChart.vue';
import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
import { computed } from 'vue';
import { ChartData, ChartOptions, Point } from 'chart.js';
import 'chartjs-adapter-dayjs-4/dist/chartjs-adapter-dayjs-4.esm';

const props = defineProps<{
  player: DetailedCashgamePlayer;
}>();

const chartOptions = computed((): ChartOptions<'line'> => {
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
        display: false,
        type: 'time',
      },
      y: {
        display: false,
      },
    },
  };
});

const chartData = computed((): ChartData<'line'> => {
  return {
    labels: props.player.actions.map((a) => a.time),
    datasets: [
      {
        label: '',
        backgroundColor: '#000000',
        borderColor: '#000000',
        spanGaps: true,
        pointStyle: false,
        borderWidth: 1,
        data: props.player.actions.map((a) => {
          var buyin = 0;
          for (let i = 0; i < props.player.actions.length; i++) {
            let action = props.player.actions[i];
            if (action.added) {
              buyin += action.added;
            }
            if (action.time.getTime() === a.time.getTime()) {
              break;
            }
          }
          var d = {
            x: a.time.getTime(),
            y: a.stack - buyin,
          } as Point;
          return d;
        }),
      },
    ],
  };
});
</script>
