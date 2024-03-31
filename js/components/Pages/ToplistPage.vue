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
          <TopListTable :games="games" :bunchId="slug" :localization="localization" />
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
import TopListTable from '@/components/TopList/TopListTable.vue';
import { Block, PageSection } from '@/components/Common';
import { computed } from 'vue';
import { useGameList, useParams, useBunch } from '@/composables';

const { slug, year } = useParams();
const { localization, bunchReady } = useBunch(slug.value);
const { getSelectedGames, gamesReady } = useGameList(slug.value);

const games = computed(() => {
  return getSelectedGames(year.value);
});

const ready = computed(() => {
  return bunchReady.value && gamesReady.value;
});
</script>
