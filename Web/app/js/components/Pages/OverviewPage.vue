<template>
  <Layout :ready="ready">
    <template slot="top-nav">
      <BunchNavigation />
    </template>

    <PageSection>
      <Block>
        <CashgameNavigation page="index" />
      </Block>
    </PageSection>

    <PageSection>
      <template slot="aside1">
        <OverviewStatus />
      </template>
      <Block>
        <PageHeading text="Current Rankings" />
        <OverviewTable v-if="hasGames" />
        <p v-else>The rankings will be displayed here when you have played your first game.</p>
      </Block>
    </PageSection>

    <PageSection :is-wide="false">
      <Block>
        <PageHeading text="Yearly Rankings" />
        <YearMatrixTable v-if="hasGames" />
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
import OverviewTable from '@/components/Overview/OverviewTable.vue';
import OverviewStatus from '@/components/Overview/OverviewStatus.vue';
import YearMatrixTable from '@/components/YearMatrix/YearMatrixTable.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import useBunches from '@/composables/useBunches';
import useUsers from '@/composables/useUsers';
import useGameArchive from '@/composables/useGameArchive';
import useCurrentGames from '@/composables/useCurrentGames';
import { computed, onMounted, watch } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();
const users = useUsers();
const bunches = useBunches();
const gameArchives = useGameArchive();
const currentGames = useCurrentGames();

const ready = computed(() => {
  return bunches.bunchReady.value && gameArchives.gamesReady.value && currentGames.currentGamesReady.value;
});

const hasGames = computed(() => {
  return gameArchives.hasGames.value;
});

const init = () => {
  users.requireUser();
  bunches.loadBunch();
  gameArchives.loadGames();
  currentGames.loadCurrentGames();
};

onMounted(() => {
  init();
});

watch(route, () => {
  init();
});
</script>
