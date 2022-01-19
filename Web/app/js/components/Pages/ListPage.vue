<template>
  <Layout :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <CashgameNavigation page="list" />
        </Block>
        <Block>
          <GameListTable />
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
import GameListTable from '@/components/GameList/GameListTable.vue';
import Block from '@/components/Common/Block.vue';
import PageSection from '@/components/Common/PageSection.vue';
import useUsers from '@/composables/useUsers';
import useBunches from '@/composables/useBunches';
import useGameArchive from '@/composables/useGameArchive';
import { computed, onMounted } from 'vue';
import { onBeforeRouteUpdate } from 'vue-router';

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

onBeforeRouteUpdate(() => {
  init();
});
</script>
