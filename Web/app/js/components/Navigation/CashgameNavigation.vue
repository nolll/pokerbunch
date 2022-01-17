<template>
  <nav class="heading-nav">
    <h1 class="page-heading">{{ selectedPageName }}</h1>
    <ul>
      <CashgameNavigationItem :url="overviewUrl" text="Overview" :isSelected="isOverviewSelected" v-on:selected="onSelected" />
      <CashgameNavigationItem :url="matrixUrl" text="Matrix" :isSelected="isMatrixSelected" v-on:selected="onSelected" />
      <CashgameNavigationItem :url="toplistUrl" text="Toplist" :isSelected="isToplistSelected" v-on:selected="onSelected" />
      <CashgameNavigationItem :url="chartUrl" text="Chart" :isSelected="isChartSelected" v-on:selected="onSelected" />
      <CashgameNavigationItem :url="listUrl" text="List" :isSelected="isListSelected" v-on:selected="onSelected" />
      <CashgameNavigationItem :url="factsUrl" text="Facts" :isSelected="isFactsSelected" v-on:selected="onSelected" />
    </ul>
    <YearDropdown
      class="year-dropdown"
      v-model="selectedYear"
      :years="years"
      v-on:input="onSelectedYear"
      v-if="isYearNavEnabled"
    />
  </nav>
</template>

<script setup lang="ts">
import CashgameNavigationItem from './CashgameNavigationItem.vue';
import { CashgamePage } from '@/models/CashgamePage';
import YearDropdown from '@/components/YearDropdown.vue';
import urls from '@/urls';
import { computed, onMounted, ref } from 'vue';
import useGameArchive from '@/composables/useGameArchive';
import useBunches from '@/composables/useBunches';
import { useRoute, useRouter } from 'vue-router';

const props = defineProps<{
  page: CashgamePage;
}>();

const bunches = useBunches();
const gameArchive = useGameArchive();
const route = useRoute();
const router = useRouter();

const selectedYear = ref<number>();

const years = computed(() => {
  return gameArchive.years.value;
});

const selectedPageName = computed(() => {
  if (props.page === CashgamePage.Matrix) return 'Matrix';
  if (props.page === CashgamePage.Toplist) return 'Toplist';
  if (props.page === CashgamePage.Chart) return 'Chart';
  if (props.page === CashgamePage.List) return 'List';
  if (props.page === CashgamePage.Facts) return 'Facts';
  return 'Overview';
});

const isYearNavEnabled = computed(() => {
  return props.page !== CashgamePage.Overview;
});

const overviewUrl = computed(() => {
  return urls.cashgame.archive(CashgamePage.Overview, bunches.slug.value);
});

const matrixUrl = computed(() => {
  return urls.cashgame.archive(CashgamePage.Matrix, bunches.slug.value, selectedYear.value);
});

const toplistUrl = computed(() => {
  return urls.cashgame.archive(CashgamePage.Toplist, bunches.slug.value, selectedYear.value);
});

const chartUrl = computed(() => {
  return urls.cashgame.archive(CashgamePage.Chart, bunches.slug.value, selectedYear.value);
});

const listUrl = computed(() => {
  return urls.cashgame.archive(CashgamePage.List, bunches.slug.value, selectedYear.value);
});

const factsUrl = computed(() => {
  return urls.cashgame.archive(CashgamePage.Facts, bunches.slug.value, selectedYear.value);
});

const isOverviewSelected = computed(() => {
  return props.page === CashgamePage.Overview;
});

const isMatrixSelected = computed(() => {
  return props.page === CashgamePage.Matrix;
});

const isToplistSelected = computed(() => {
  return props.page === CashgamePage.Toplist;
});

const isChartSelected = computed(() => {
  return props.page === CashgamePage.Chart;
});

const isListSelected = computed(() => {
  return props.page === CashgamePage.List;
});

const isFactsSelected = computed(() => {
  return props.page === CashgamePage.Facts;
});

const onSelected = (url: string) => {
  if (route.fullPath !== url) router.push(url);
};

const onSelectedYear = () => {
  router.push(urls.cashgame.archive(props.page, bunches.slug.value, selectedYear.value));
};

onMounted(() => {
  selectedYear.value = gameArchive.selectedYear.value || gameArchive.currentYear.value;
});
</script>

<style lang="scss" scoped>
.year-dropdown {
  margin-top: 10px;
}
</style>
