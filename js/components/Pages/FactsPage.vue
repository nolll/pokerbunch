<template>
  <Layout :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <CashgameNavigation :year="year" :slug="slug" :years="years" page="facts" />
        </Block>
      </PageSection>

      <PageSection>
        <template v-slot:default>
          <Block>
            <SingleGameFacts :slug="slug" :games="games" />
          </Block>
          <Block>
            <TotalFacts :slug="slug" :games="games" />
          </Block>
        </template>
        <template v-slot:aside2>
          <Block>
            <OverallFacts :slug="slug" :games="games" />
          </Block>
        </template>
      </PageSection>
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
import { computed, onMounted } from 'vue';
import auth from '@/auth';
import { filterGames, getYears } from '@/helpers/gameArchiveHelpers';
import { useGameArchiveQuery } from '@/queries/gameArchiveQueries';
import useParams from '@/helpers/useParams';

const params = useParams();
const slug = computed(() => params.slug.value);
const year = computed(() => params.year.value);
const gameArchiveQuery = useGameArchiveQuery(slug.value);
const years = computed(() => getYears(allGames.value));
const allGames = computed(() => gameArchiveQuery.data.value ?? []);

const games = computed(() => {
  return filterGames(allGames.value, params.year.value);
});

const ready = computed(() => {
  return gameArchiveQuery.isSuccess.value;
});

onMounted(() => auth.requireUser());
</script>
