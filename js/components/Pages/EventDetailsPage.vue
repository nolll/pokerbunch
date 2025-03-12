<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <PageHeading :text="name" />
        </Block>
        <Block>
          <MatrixTable :slug="slug" :games="eventGames" :localization="localization" />
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import { Block, PageHeading, PageSection } from '@/components/Common';
import MatrixTable from '@/components/Matrix/MatrixTable.vue';
import { computed } from 'vue';
import { useParams, useBunch, useEventList, useEventGameList } from '@/composables';

const { slug, eventId } = useParams();
const { getEvent, eventsReady } = useEventList(slug.value);
const { localization, bunchReady } = useBunch(slug.value);
const { eventGames, eventGamesReady } = useEventGameList(slug.value, eventId.value);

const name = computed(() => {
  if (event.value) return event.value.name;
  return '';
});

const event = computed(() => getEvent(eventId.value));
const ready = computed(() => bunchReady.value && eventsReady.value && eventGamesReady.value);
</script>
