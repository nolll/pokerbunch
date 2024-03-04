<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <CashgameNavigation page="toplist" />
        </Block>
        <Block>
          <TopListTable :games="games" :bunchId="slug" />
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
import TopListTable from '@/components/TopList/TopListTable.vue';
import Block from '@/components/Common/Block.vue';
import PageSection from '@/components/Common/PageSection.vue';
import { computed, onMounted } from 'vue';
import useBunches from '@/composables/useBunches';
import { onBeforeRouteUpdate, useRoute } from 'vue-router';
import useGameList from '@/composables/useGameList';
import useParams from '@/composables/useParams';

const { slug, year } = useParams();
const { getSelectedGames, gamesReady } = useGameList(slug.value);

const games = computed(() => {
  return getSelectedGames(year.value);
});

const ready = computed(() => {
  return gamesReady.value;
});
</script>
