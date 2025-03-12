<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <CashgameNavigation page="list" />
        </Block>
        <Block>
          <GameListTable :bunch="bunch" :games="games" :localization="localization" />
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
import GameListTable from '@/components/GameList/GameListTable.vue';
import { Block, PageSection } from '@/components/Common';
import { computed } from 'vue';
import { useParams, useBunch, useGameList } from '@/composables';

const { slug, year } = useParams();
const { bunch, localization, bunchReady } = useBunch(slug.value);
const { getSelectedGames, gamesReady } = useGameList(slug.value);

const games = computed(() => getSelectedGames(year.value));
const ready = computed(() => bunchReady.value && gamesReady.value);
</script>
