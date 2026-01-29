<template>
  <div>
    <Line v-show="ready" :data="chartData" :options="chartOptions" />
    <LoadingSpinner v-show="!ready"></LoadingSpinner>
  </div>
</template>

<script setup lang="ts">
import { LoadingSpinner } from '@/components/Common';
import { NewChartData } from '@/models/ChartData';
import { NewChartOptions } from '@/models/ChartOptions';
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
} from 'chart.js';

ChartJS.register(Title, Tooltip, Legend, PointElement, LineElement, BarElement, CategoryScale, LinearScale, Colors);

const props = defineProps<{
  chartData: NewChartData;
  chartOptions: NewChartOptions;
  ready: boolean;
}>();
</script>
