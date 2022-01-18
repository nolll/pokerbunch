<template>
  <Layout :ready="ready">
    <template slot="top-nav">
      <BunchNavigation />
    </template>

    <PageSection>
      <Block>
        <CashgameNavigation page="facts" />
      </Block>
    </PageSection>

    <PageSection>
      <Block>
        <SingleGameFacts />
      </Block>
      <Block>
        <TotalFacts />
      </Block>
      <template slot="aside2">
        <Block>
          <OverallFacts />
        </Block>
      </template>
    </PageSection>

    <template slot="main">
      <PageSection> </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
import SingleGameFacts from '@/components/Facts/SingleGameFacts.vue';
import TotalFacts from '@/components/Facts/TotalFacts.vue';
import OverallFacts from '@/components/Facts/OverallFacts.vue';
import Block from '@/components/Common/Block.vue';
import PageSection from '@/components/Common/PageSection.vue';
import useUsers from '@/composables/useUsers';
import useBunches from '@/composables/useBunches';
import useGameArchive from '@/composables/useGameArchive';
import { computed, onMounted, watch } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();
const users = useUsers();
const bunches = useBunches();
const gameArchive = useGameArchive();

const ready = computed(() => {
  return bunches.bunchReady.value && gameArchive.gamesReady.value;
});

const init = () => {
  users.requireUser();
  bunches.loadBunch();
  gameArchive.loadGames();
};

onMounted(() => {
  init();
});

watch(route, () => {
  init();
});
</script>
