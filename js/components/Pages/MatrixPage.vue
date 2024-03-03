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
import useGameList from '@/composables/useGameList';
import useParams from '@/composables/useParams';

const params = useParams();
const { getSelectedGames, gamesReady } = useGameList(params.slug.value);

const slug = computed(() => params.slug.value);
const games = computed(() => getSelectedGames(params.year.value));
const ready = computed(() => gamesReady.value);
</script>
