<template>
  <Layout :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <CashgameNavigation page="chart" />
        </Block>
      </PageSection>

      <PageSection>
        <Block>
          <CashgameChart />
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
import CashgameChart from '@/components/CashgameChart.vue';
import Block from '@/components/Common/Block.vue';
import PageSection from '@/components/Common/PageSection.vue';
import useBunches from '@/composables/useBunches';
import useGameArchive from '@/composables/useGameArchive';
import useUsers from '@/composables/useUsers';
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
