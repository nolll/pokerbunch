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
      class="cashgame-navigation__year-dropdown"
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
  if (props.page === 'matrix') return 'Matrix';
  if (props.page === 'toplist') return 'Toplist';
  if (props.page === 'chart') return 'Chart';
  if (props.page === 'list') return 'List';
  if (props.page === 'facts') return 'Facts';
  return 'Overview';
});

const isYearNavEnabled = computed(() => {
  return props.page !== 'index';
});

const overviewUrl = computed(() => {
  return urls.cashgame.index(bunches.slug.value);
});

const matrixUrl = computed(() => {
  return urls.cashgame.archive('matrix', bunches.slug.value, selectedYear.value);
});

const toplistUrl = computed(() => {
  return urls.cashgame.archive('toplist', bunches.slug.value, selectedYear.value);
});

const chartUrl = computed(() => {
  return urls.cashgame.archive('chart', bunches.slug.value, selectedYear.value);
});

const listUrl = computed(() => {
  return urls.cashgame.archive('list', bunches.slug.value, selectedYear.value);
});

const factsUrl = computed(() => {
  return urls.cashgame.archive('facts', bunches.slug.value, selectedYear.value);
});

const isOverviewSelected = computed(() => {
  return props.page === 'index';
});

const isMatrixSelected = computed(() => {
  return props.page === 'matrix';
});

const isToplistSelected = computed(() => {
  return props.page === 'toplist';
});

const isChartSelected = computed(() => {
  return props.page === 'chart';
});

const isListSelected = computed(() => {
  return props.page === 'list';
});

const isFactsSelected = computed(() => {
  return props.page === 'facts';
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

<style lang="scss">
.cashgame-navigation__year-dropdown {
  margin-top: 10px;
}
</style>
