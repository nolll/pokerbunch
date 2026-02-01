<template>
  <div>
    <LineChart :chart-data="chartData2" :chart-options="chartOptions" :ready="true" />
  </div>
</template>

<script setup lang="ts">
import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
import { computed } from 'vue';
import { ChartData, ChartOptions, Point } from 'chart.js';
import LineChart from '@/components/LineChart.vue';

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

const chartData2 = computed((): ChartData<'line'> => {
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
        data: chartDatasets.value.buyinData,
      },
      {
        label: 'Stack',
        backgroundColor: '#000000',
        borderColor: '#000000',
        pointRadius: 0,
        borderWidth: 1,
        data: chartDatasets.value.stackData,
      },
    ],
  };
});

const chartDatasets = computed(() => {
  const buyinPoints: Point[] = [];
  const stackPoints: Point[] = [];
  var buyin = 0;
  for (let i = 0; i < props.player.actions.length; i++) {
    const action = props.player.actions[i];
    const added = action.added || 0;
    const time = action.time.getTime();
    if (added > 0) {
      let stackBeforeBuyin = action.stack - added;
      buyinPoints.push({
        x: time,
        y: buyin,
      });
      if (buyin > 0) {
        stackPoints.push({
          x: time,
          y: stackBeforeBuyin,
        });
      }
    }
    buyin += added;
    buyinPoints.push({
      x: time,
      y: buyin,
    });
    stackPoints.push({
      x: time,
      y: action.stack,
    });
  }

  return {
    buyinData: buyinPoints,
    stackData: stackPoints,
  };
});
</script>
