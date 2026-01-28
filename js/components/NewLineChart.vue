<template>
  <div>
    <Line :data="chartData" :options="chartOptions2" />
    <LoadingSpinner v-show="!ready"></LoadingSpinner>
  </div>
</template>

<script setup lang="ts">
import { debounce } from 'ts-debounce';
import { GoogleCharts } from 'google-charts';
import { LoadingSpinner } from '@/components/Common';
import { NewChartData } from '@/models/ChartData';
import { NewChartOptions } from '@/models/ChartOptions';
import { computed, nextTick, onMounted, ref, watch } from 'vue';
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
  ready: Boolean;
}>();

const chartOptions2 = {
  responsive: true,
};

const chart = ref<any>();
chart.value = null;
const chartsLoaded = ref(false);
const container = ref<HTMLElement>();

const ready = computed(() => {
  return dataLoaded.value && chartsLoaded.value;
});

const dataLoaded = computed(() => {
  return !!props.chartData;
});

const loadCharts = () => {
  GoogleCharts.load(() => {
    createChart();
  });
};

const createChart = () => {
  chart.value = new GoogleCharts.api.visualization.LineChart(container.value);
  initResizeHandler();
  chartsLoaded.value = true;
};

const draw = () => {
  var dataTable = new GoogleCharts.api.visualization.DataTable(props.chartData);
  chart.value.draw(dataTable, getConfig());
};

const initResizeHandler = () => {
  window.addEventListener(
    'resize',
    debounce((event: Event) => {
      draw();
    }, 150)
  );
};

const getConfig = () => {
  var conf = {
    backgroundColor: '#fff',
    fontSize: 16,
    fontName: 'arial',
    interpolateNulls: true,
    lineWidth: 1,
    pointSize: 2,
    theme: 'maximized',
    seriesType: 'line',
    width: getWidth(),
    height: getHeight(),
  };

  if (typeof props.chartOptions == 'object') {
    return Object.assign(conf, props.chartOptions);
  }

  return conf;
};

const getWidth = () => (Boolean(container.value) ? parseInt(window.getComputedStyle(container.value!).width) : 0);

const getHeight = () => getWidth() / 2;

onMounted(async () => {
  await nextTick();
  console.log(props.chartData);
  loadCharts();
});

watch(chartsLoaded, () => {
  if (ready.value) {
    draw();
  }
});

// todo: Do we need a function to watch a prop?
watch(
  () => props.chartData,
  () => {
    if (ready.value) {
      draw();
    }
  }
);
</script>
