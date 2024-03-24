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
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
import GameListTable from '@/components/GameList/GameListTable.vue';
import Block from '@/components/Common/Block.vue';
import PageSection from '@/components/Common/PageSection.vue';
import { computed } from 'vue';
import useParams from '@/composables/useParams';
import useBunch from '@/composables/useBunch';
import useGameList from '@/composables/useGameList';

const { slug, year } = useParams();
const { bunch, localization, bunchReady } = useBunch(slug.value);
const { getSelectedGames, gamesReady } = useGameList(slug.value);

const games = computed(() => {
  return getSelectedGames(year.value);
});

const ready = computed(() => {
  return bunchReady.value && gamesReady.value;
});
</script>
