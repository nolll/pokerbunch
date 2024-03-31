<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <CashgameNavigation page="matrix" />
        </Block>
      </PageSection>

      <PageSection>
        <Block>
          <MatrixTable :slug="slug" :games="games" :localization="localization" />
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import CashgameNavigation from '@/components/Navigation/CashgameNavigation.vue';
import MatrixTable from '@/components/Matrix/MatrixTable.vue';
import { Block, PageSection } from '@/components/Common';
import { computed } from 'vue';
import { useGameList, useParams, useBunch } from '@/composables';

const { slug, year } = useParams();
const { localization, bunchReady } = useBunch(slug.value);
const { getSelectedGames, gamesReady } = useGameList(slug.value);

const games = computed(() => getSelectedGames(year.value));
const ready = computed(() => gamesReady.value);
</script>
