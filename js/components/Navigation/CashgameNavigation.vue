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
import { useRoute, useRouter } from 'vue-router';
import { useGameList, useParams } from '@/composables';

const props = defineProps<{
  page: CashgamePage;
}>();

const { slug, year } = useParams();
const route = useRoute();
const router = useRouter();

const selectedYear = ref<number>();
const { years, currentYear } = useGameList(slug.value);

const selectedPageName = computed(() => {
  if (props.page === 'matrix') return 'Matrix';
  if (props.page === 'toplist') return 'Toplist';
  if (props.page === 'chart') return 'Chart';
  if (props.page === 'list') return 'List';
  if (props.page === 'facts') return 'Facts';
  return 'Overview';
});

const isYearNavEnabled = computed(() => props.page !== 'index');
const overviewUrl = computed(() => urls.cashgame.index(slug.value));
const matrixUrl = computed(() => urls.cashgame.archive('matrix', slug.value, selectedYear.value));
const toplistUrl = computed(() => urls.cashgame.archive('toplist', slug.value, selectedYear.value));
const chartUrl = computed(() => urls.cashgame.archive('chart', slug.value, selectedYear.value));
const listUrl = computed(() => urls.cashgame.archive('list', slug.value, selectedYear.value));
const factsUrl = computed(() => urls.cashgame.archive('facts', slug.value, selectedYear.value));
const isOverviewSelected = computed(() => props.page === 'index');
const isMatrixSelected = computed(() => props.page === 'matrix');
const isToplistSelected = computed(() => props.page === 'toplist');
const isChartSelected = computed(() => props.page === 'chart');
const isListSelected = computed(() => props.page === 'list');
const isFactsSelected = computed(() => props.page === 'facts');

const onSelected = (url: string) => {
  if (route.fullPath !== url) router.push(url);
};

const onSelectedYear = () => {
  router.push(urls.cashgame.archive(props.page, slug.value, selectedYear.value));
};

onMounted(() => {
  selectedYear.value = year.value || currentYear.value;
});
</script>

<style lang="scss">
.cashgame-navigation__year-dropdown {
  margin-top: 10px;
}
</style>
