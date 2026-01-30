<template>
  <div>
    <Line v-show="ready" :data="chartData" :options="chartOptions" />
    <LoadingSpinner v-show="!ready"></LoadingSpinner>
  </div>
</template>

<script setup lang="ts">
import { LoadingSpinner } from '@/components/Common';
import { Line } from 'vue-chartjs';
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  PointElement,
  LineElement,
  CategoryScale,
  LinearScale,
  Colors,
  ChartData,
  ChartOptions,
  TimeScale,
} from 'chart.js';

ChartJS.register(Title, Tooltip, Legend, PointElement, LineElement, BarElement, CategoryScale, LinearScale, TimeScale, Colors);

const props = defineProps<{
  chartData: ChartData<'line'>;
  chartOptions: ChartOptions<'line'>;
  ready: boolean;
}>();
</script>
