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
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import MatrixTable from '@/components/Matrix/MatrixTable.vue';
import { computed } from 'vue';
import useParams from '@/composables/useParams';
import useBunch from '@/composables/useBunch';
import useEventList from '@/composables/useEventList';
import useEventGameList from '@/composables/useEventGameList';

const { slug, eventId } = useParams();
const { getEvent, eventsReady } = useEventList(slug.value);
const { localization, bunchReady } = useBunch(slug.value);
const { eventGames, eventGamesReady } = useEventGameList(slug.value, eventId.value);

const name = computed(() => {
  if (event.value) return event.value.name;
  return '';
});

const event = computed(() => {
  return getEvent(eventId.value);
});

const ready = computed(() => {
  return bunchReady.value && eventsReady.value && eventGamesReady.value;
});
</script>
