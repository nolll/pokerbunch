<template>
  <div ref="container">
    <div ref="placeholder"></div>
    <LoadingSpinner v-show="!ready"></LoadingSpinner>
  </div>
</template>

<script setup lang="ts">
import { debounce } from 'ts-debounce';
import { GoogleCharts } from 'google-charts';
import LoadingSpinner from '@/components/Common/LoadingSpinner.vue';
import { ChartData } from '@/models/ChartData';
import { ChartOptions } from '@/models/ChartOptions';
import { computed, nextTick, onMounted, ref, watch } from 'vue';

const props = defineProps<{
  chartData: ChartData | null;
  chartOptions: ChartOptions;
}>();

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

const getWidth = () => {
  if (container.value) return parseInt(window.getComputedStyle(container.value).width);
  return 0;
};

const getHeight = () => {
  return getWidth() / 2;
};

onMounted(async () => {
  await nextTick();
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
