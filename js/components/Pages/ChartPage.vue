<template>
  <Layout :require-user="true" :ready="ready">
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
          <CashgameChart :games="games" />
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
import CashgameChart from '@/components/CashgameChart.vue';
import { Block, PageSection } from '@/components/Common';
import { computed } from 'vue';
import { useParams, useGameList } from '@/composables';

const { slug, year } = useParams();
const { getSelectedGames, gamesReady } = useGameList(slug.value);

const games = computed(() => getSelectedGames(year.value));

const ready = computed(() => {
  return gamesReady.value;
});
</script>
