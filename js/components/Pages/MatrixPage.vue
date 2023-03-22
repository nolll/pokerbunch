<template>
  <Layout :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <CashgameNavigation :years="years" page="matrix" />
        </Block>
      </PageSection>

      <PageSection>
        <Block>
          <MatrixTable :slug="slug" :games="games" />
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
import MatrixTable from '@/components/Matrix/MatrixTable.vue';
import Block from '@/components/Common/Block.vue';
import PageSection from '@/components/Common/PageSection.vue';
import { computed } from 'vue';
import { useGameArchiveQuery } from '@/queries/gameArchiveQueries';
import useParams from '@/helpers/useParams';
import { filterGames, getYears } from '@/helpers/gameArchiveHelpers';
import gameSorter from '@/GameSorter';
import { CashgameSortOrder } from '@/models/CashgameSortOrder';
import auth from '@/auth';

auth.requireUser();

const params = useParams();
const gameArchiveQuery = useGameArchiveQuery(params.slug.value);

const slug = computed(() => params.slug.value);
const years = computed(() => getYears(allGames.value));

const allGames = computed(() => gameArchiveQuery.data.value ?? []);

const games = computed(() => {
  return gameSorter.sort(filterGames(allGames.value, params.year.value), CashgameSortOrder.Date);
});

const ready = computed(() => {
  return gameArchiveQuery.isSuccess.value;
});
</script>
