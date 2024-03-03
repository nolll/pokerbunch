<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <CashgameNavigation page="index" />
        </Block>
      </PageSection>

      <PageSection>
        <template v-slot:aside1>
          <OverviewStatus />
        </template>
        <template v-slot:default>
          <Block>
            <PageHeading text="Current Rankings" />
            <OverviewTable v-if="hasGames" :games="games" />
            <p v-else>The rankings will be displayed here when you have played your first game.</p>
          </Block>
        </template>
      </PageSection>

      <PageSection :is-wide="false">
        <Block>
          <PageHeading v-if="hasGames" text="Yearly Rankings" />
          <YearMatrixTable v-if="hasGames" />
        </Block>
      </PageSection>
    </template>
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
import useGameArchive from '@/composables/useGameArchive';
import useCurrentGames from '@/composables/useCurrentGames';
import { computed, onMounted } from 'vue';

const bunches = useBunches();
const gameArchive = useGameArchive();
const currentGames = useCurrentGames();

const ready = computed(() => {
  return bunches.bunchReady.value && gameArchive.gamesReady.value && currentGames.currentGamesReady.value;
});

const hasGames = computed(() => {
  return gameArchive.hasGames.value;
});

const init = async () => {
  bunches.loadBunch();
  gameArchive.loadGames();
  gameArchive.selectYear(undefined);
  currentGames.loadCurrentGames();
};

onMounted(async () => {
  init();
});
</script>
