<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <CashgameNavigation page="matrix" />
        </Block>
      </PageSection>

      <PageSection>
        <Block>
          <MatrixTable :slug="slug" :games="games" />
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
import MatrixTable from '@/components/Matrix/MatrixTable.vue';
import Block from '@/components/Common/Block.vue';
import PageSection from '@/components/Common/PageSection.vue';
import useGameArchive from '@/composables/useGameArchive';
import useBunches from '@/composables/useBunches';
import useUsers from '@/composables/useUsers';
import { computed, onMounted } from 'vue';
import { onBeforeRouteUpdate, useRoute } from 'vue-router';

const route = useRoute();
const users = useUsers();
const bunches = useBunches();
const gameArchive = useGameArchive();

const slug = computed(() => {
  return bunches.slug.value;
});

const games = computed(() => {
  return gameArchive.sortedGames.value;
});

const ready = computed(() => {
  return bunches.bunchReady.value && gameArchive.gamesReady.value;
});

const init = (year: number | undefined) => {
  users.requireUser();
  bunches.loadBunch();
  gameArchive.loadGames();
  gameArchive.selectYear(year);
};

const getSelectedYear = (s: string | undefined) => {
  if (!s || s === '') return undefined;
  return parseInt(s);
};

onMounted(() => {
  init(getSelectedYear(route.params.year as string));
});

onBeforeRouteUpdate(async (to) => {
  init(getSelectedYear(to.params.year as string));
});
</script>
