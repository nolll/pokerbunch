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
import { ChartData, ChartOptions, Point } from 'chart.js';
import { getColor } from '@/colors';

interface ChartPlayerResult {
  time: Date;
  winnings: number;
}

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
    //labels: props.player.actions.map((a) => a.time),
    datasets: chartDatasets.value,
  };
});

const chartDatasets = computed(() => {
  const datasets = [];
  for (let i = 0; i < props.players.length; i++) {
    const player = props.players[i];
    datasets.push(getPlayerDataset(player, getColor(i)));
  }

  return datasets;
});

const getPlayerDataset = (player: DetailedCashgamePlayer, color: string) => {
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
    pointRadius: 0,
    borderWidth: 3,
    data: points,
  };
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
</script>
