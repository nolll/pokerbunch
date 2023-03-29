<template>
  <Layout :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <CashgameNavigation :year="year" :slug="slug" :years="years" page="matrix" />
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
import { computed, onMounted, provide } from 'vue';
import { useGameArchiveQuery } from '@/queries/gameArchiveQueries';
import useParams from '@/helpers/useParams';
import { filterGames, getYears } from '@/helpers/gameArchiveHelpers';
import auth from '@/auth';
import { useBunchQuery } from '@/queries/bunchQueries';
import { bunchKey } from '@/helpers/injectionKeys';

const params = useParams();
const slug = computed(() => params.slug.value);
const gameArchiveQuery = useGameArchiveQuery(slug.value);
const bunchQuery = useBunchQuery(slug.value);
const year = computed(() => params.year.value);
const years = computed(() => getYears(allGames.value));
const allGames = computed(() => gameArchiveQuery.data.value ?? []);

const games = computed(() => {
  return filterGames(allGames.value, params.year.value);
});

const bunch = computed(() => bunchQuery.data.value!);

const ready = computed(() => {
  return bunchQuery.isSuccess.value && gameArchiveQuery.isSuccess.value;
});

provide(bunchKey, bunch);

onMounted(() => auth.requireUser());
</script>
