<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <CashgameNavigation page="facts" />
        </Block>
      </PageSection>

      <PageSection>
        <template v-slot:default>
          <Block>
            <SingleGameFacts :games="games" :localization="localization" />
          </Block>
          <Block>
            <TotalFacts :games="games" :localization="localization" />
          </Block>
        </template>
        <template v-slot:aside2>
          <Block>
            <OverallFacts :games="games" :localization="localization" />
          </Block>
        </template>
      </PageSection>
    </template>

    <template v-slot:main>
      <PageSection> </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
import SingleGameFacts from '@/components/Facts/SingleGameFacts.vue';
import TotalFacts from '@/components/Facts/TotalFacts.vue';
import OverallFacts from '@/components/Facts/OverallFacts.vue';
import Block from '@/components/Common/Block.vue';
import PageSection from '@/components/Common/PageSection.vue';
import { computed } from 'vue';
import useGameList from '@/composables/useGameList';
import useParams from '@/composables/useParams';
import useBunch from '@/composables/useBunch';

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
