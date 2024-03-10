<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <CashgameNavigation page="index" />
        </Block>
      </PageSection>

      <PageSection>
        <template v-slot:aside1>
          <OverviewStatus :games="currentGames" />
        </template>
        <template v-slot:default>
          <Block>
            <PageHeading text="Current Rankings" />
            <OverviewTable v-if="hasGames" :bunch="bunch" :games="currentYearGames" :localization="localization" />
            <p v-else>The rankings will be displayed here when you have played your first game.</p>
          </Block>
        </template>
      </PageSection>

      <PageSection :is-wide="false">
        <Block>
          <PageHeading v-if="hasGames" text="Yearly Rankings" />
          <YearMatrixTable v-if="hasGames" :bunch="bunch" :games="allGames" :localization="localization" />
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
import { computed } from 'vue';
import useParams from '@/composables/useParams';
import useBunch from '@/composables/useBunch';
import useGameList from '@/composables/useGameList';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import archiveHelper from '@/ArchiveHelper';
import gameSorter from '@/GameSorter';
import { CashgameSortOrder } from '@/models/CashgameSortOrder';
import useCurrentGameList from '@/composables/useCurrentGameList';

const { slug } = useParams();
const { bunch, localization, bunchReady } = useBunch(slug.value);
const { allGames, getSelectedGames, hasGames, gamesReady } = useGameList(slug.value);
const { currentGames, currentGamesReady } = useCurrentGameList(slug.value);

const ready = computed(() => {
  return bunchReady.value && gamesReady.value && currentGamesReady.value;
});

const currentYearGames = computed((): ArchiveCashgame[] => {
  const selectedGames = getSelectedGames(currentYear.value);
  return gameSorter.sort(selectedGames, CashgameSortOrder.Date);
});

const currentYear = computed(() => {
  return archiveHelper.getCurrentYear(allGames.value);
});
</script>
