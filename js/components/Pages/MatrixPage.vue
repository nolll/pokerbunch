﻿<template>
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
import Block from '@/components/Common/Block.vue';
import PageSection from '@/components/Common/PageSection.vue';
import { computed } from 'vue';
import useGameList from '@/composables/useGameList';
import useParams from '@/composables/useParams';
import useBunch from '@/composables/useBunch';

const { slug, year } = useParams();
const { localization, bunchReady } = useBunch(slug.value);
const { getSelectedGames, gamesReady } = useGameList(slug.value);

const games = computed(() => getSelectedGames(year.value));
const ready = computed(() => gamesReady.value);
</script>
