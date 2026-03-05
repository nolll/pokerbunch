<template>
  <div class="chart-container">
    <LineChart :chart-data="chartData" :chart-options="chartOptions" :ready="true" />
  </div>
</template>

<script setup lang="ts">
import LineChart from '@/components/LineChart.vue';
import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
import { computed } from 'vue';
import { ChartData, ChartOptions, Point } from 'chart.js';

const props = defineProps<{
  player: DetailedCashgamePlayer;
}>();

const chartOptions = computed((): ChartOptions<'line'> => {
  return {
    maintainAspectRatio: false,
    responsive: true,
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
        borderColor: '#999999',
        spanGaps: true,
        pointStyle: false,
        borderWidth: 2,
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

<style lang="scss" scoped>
.chart-container {
  position: relative;
  width: 100%;
  height: 60px;
}
</style>
