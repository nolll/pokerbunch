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
          <MatrixTable :slug="slug" :games="games" :localization="localization" />
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
import api from '@/api';
import MatrixTable from '@/components/Matrix/MatrixTable.vue';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import useEvents from '@/composables/useEvents';
import { useRoute } from 'vue-router';
import { computed, onMounted, ref } from 'vue';
import useParams from '@/composables/useParams';
import useBunch from '@/composables/useBunch';

const { slug } = useParams();
const route = useRoute();
const events = useEvents();
const { localization, bunchReady } = useBunch(slug.value);

const games = ref<ArchiveCashgame[]>([]);

const name = computed(() => {
  if (event.value) return event.value.name;
  return '';
});

const event = computed(() => {
  for (let i = 0; i < events.events.value.length; i++) {
    const event = events.events.value[i];
    if (event.id.toString() === eventId.value) return event;
  }
  return null;
});

const eventId = computed(() => {
  return route.params.id as string;
});

const ready = computed(() => {
  return bunchReady.value && events.eventsReady.value;
});

const init = async () => {
  events.loadEvents();
  await loadGames();
};

const loadGames = async () => {
  try {
    const response = await api.getEventGames(slug.value, eventId.value);
    games.value = response.data.map((o) => ArchiveCashgame.fromResponse(o));
  } catch {
    games.value = [];
  }
};

onMounted(async () => {
  await init();
});
</script>
