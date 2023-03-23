<template>
  <Layout :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <CashgameNavigation :year="latestYear" :slug="slug" :years="years" page="index" />
        </Block>
      </PageSection>

      <PageSection>
        <template v-slot:aside1>
          <OverviewStatus />
        </template>
        <template v-slot:default>
          <Block>
            <PageHeading text="Current Rankings" />
            <OverviewTable :year="latestYear" :slug="slug" v-if="hasGames" :games="allGames" />
            <p v-else>The rankings will be displayed here when you have played your first game.</p>
          </Block>
        </template>
      </PageSection>

      <PageSection :is-wide="false">
        <Block>
          <PageHeading v-if="hasGames" text="Yearly Rankings" />
          <YearMatrixTable :years="years" :slug="slug" :current-year="latestYear" v-if="hasGames" :games="allGames" />
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
import OverviewTable from '@/components/Overview/OverviewTable.vue';
import OverviewStatus from '@/components/Overview/OverviewStatus.vue';
import YearMatrixTable from '@/components/YearMatrix/YearMatrixTable.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import useBunches from '@/composables/useBunches';
import useUsers from '@/composables/useUsers';
import useGameArchive from '@/composables/useGameArchive';
import useCurrentGames from '@/composables/useCurrentGames';
import { computed, onMounted } from 'vue';
import { useGameArchiveQuery } from '@/queries/gameArchiveQueries';
import useParams from '@/helpers/useParams';
import { getYears } from '@/helpers/gameArchiveHelpers';
import auth from '@/auth';

auth.requireUser();

const params = useParams();
const users = useUsers();
const bunches = useBunches();
const gameArchive = useGameArchive();
const currentGames = useCurrentGames();

const slug = computed(() => params.slug.value);
const gameArchiveQuery = useGameArchiveQuery(slug.value);
const years = computed(() => getYears(allGames.value));
const latestYear = computed(() => years.value[0]);

const allGames = computed(() => gameArchiveQuery.data.value ?? []);

const ready = computed(() => {
  return gameArchiveQuery.isSuccess.value && currentGames.currentGamesReady.value;
});

const hasGames = computed(() => {
  return allGames.value.length > 0;
});

const init = async () => {
  users.requireUser();
  bunches.loadBunch();
  gameArchive.loadGames();
  gameArchive.selectYear(undefined);
  currentGames.loadCurrentGames();
};

onMounted(async () => {
  init();
});
</script>
