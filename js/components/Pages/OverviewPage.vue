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
          <OverviewStatus :games="currentGames" />
        </template>
        <template v-slot:default>
          <Block>
            <PageHeading text="Current Rankings" />
            <OverviewTable :year="latestYear" :slug="slug" v-if="hasGames" :games="latestYearGames" />
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
import { computed, onMounted, provide } from 'vue';
import { useGameArchiveQuery } from '@/queries/gameArchiveQueries';
import useParams from '@/helpers/useParams';
import { filterGames, getYears } from '@/helpers/gameArchiveHelpers';
import auth from '@/auth';
import { useCurrentGamesQuery } from '@/queries/currentGameQueries';
import { useBunchQuery } from '@/queries/bunchQueries';
import { bunchKey } from '@/helpers/injectionKeys';

const params = useParams();
const slug = computed(() => params.slug.value);
const currentGamesQuery = useCurrentGamesQuery(slug.value);
const gameArchiveQuery = useGameArchiveQuery(slug.value);
const bunchQuery = useBunchQuery(slug.value);
const years = computed(() => getYears(allGames.value));
const latestYear = computed(() => years.value[0]);

const bunch = computed(() => bunchQuery.data.value!);
const latestYearGames = computed(() => filterGames(allGames.value, latestYear.value));
const allGames = computed(() => gameArchiveQuery.data.value ?? []);
const currentGames = computed(() => currentGamesQuery.data.value ?? []);

const ready = computed(() => {
  return bunchQuery.isSuccess.value && gameArchiveQuery.isSuccess.value && currentGamesQuery.isSuccess.value;
});

const hasGames = computed(() => allGames.value.length > 0);

provide(bunchKey, bunch);

onMounted(() => auth.requireUser());
</script>
