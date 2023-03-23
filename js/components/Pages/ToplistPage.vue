<template>
  <Layout :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <CashgameNavigation :year="year" :slug="slug" :years="years" page="toplist" />
        </Block>
        <Block>
          <TopListTable :slug="slug" :games="allGames" />
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
import useParams from '@/helpers/useParams';
import { useGameArchiveQuery } from '@/queries/gameArchiveQueries';
import { getYears } from '@/helpers/gameArchiveHelpers';

var params = useParams();
const gameArchiveQuery = useGameArchiveQuery(params.slug.value);
const years = computed(() => getYears(allGames.value));
const allGames = computed(() => gameArchiveQuery.data.value ?? []);
const slug = computed(() => params.slug.value);
const year = computed(() => params.year.value);

const ready = computed(() => {
  return gameArchiveQuery.isSuccess.value;
});
</script>
