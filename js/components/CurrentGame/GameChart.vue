<template>
  <div>
    <LineChart :chart-data="chartData" :chart-options="chartOptions" :ready="true" />
  </div>
</template>

<script setup lang="ts">
import dayjs from 'dayjs';
import utc from 'dayjs/plugin/utc';
import LineChart from '@/components/LineChart.vue';
import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
import { computed } from 'vue';
import { ChartData, ChartDataset, ChartOptions, Point } from 'chart.js';
import { getColor } from '@/colors';

dayjs.extend(utc);

const props = defineProps<{
  players: DetailedCashgamePlayer[];
}>();

const chartOptions = computed((): ChartOptions<'line'> => {
  return {
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

const chartData = computed((): ChartData<'line'> => {
  return {
    datasets: chartDatasets.value,
  };
});

const chartDatasets = computed((): ChartDataset<'line'>[] => {
  const datasets = [];
  for (let i = 0; i < props.players.length; i++) {
    const player = props.players[i];
    datasets.push(getPlayerDataset(player, playerColors.value[player.id]));
  }

  return datasets;
});

const getPlayerDataset = (player: DetailedCashgamePlayer, color: string): ChartDataset<'line'> => {
  const points: Point[] = [];
  var results = getPlayerResults(player);
  for (let i = 0; i < results.length; i++) {
    const result = results[i];
    const time = result.time.getTime();
    points.push({
      x: time,
      y: result.winnings,
    });
  }

  if (!player.hasCashedOut()) {
    points.push({
      x: dayjs.utc().toDate().getTime(),
      y: results[results.length - 1].winnings,
    });
  }

  return {
    label: player.name,
    backgroundColor: color,
    borderColor: color,
    pointStyle: false,
    borderWidth: 3,
    data: points,
  };
};

const playerColors = computed(() => {
  const colors = {} as Record<string, string>;
  const sorted = [...props.players].sort((a, b) => a.name.localeCompare(b.name));
  for (let i = 0; i < sorted.length; i++) {
    colors[sorted[i].id] = getColor(i);
  }
  return colors;
});

const getPlayerResults = (player: DetailedCashgamePlayer) => {
  let added = 0;
  const results = player.actions.map((a) => {
    added += a.added || 0;
    return {
      time: a.time,
      winnings: a.stack - added,
    };
  });
  return results;
};
</script>
