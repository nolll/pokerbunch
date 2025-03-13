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
          <DefinitionList>
            <DefinitionTerm>Location</DefinitionTerm>
            <DefinitionData>{{ location }}</DefinitionData>

            <DefinitionTerm>Total Time Played</DefinitionTerm>
            <DefinitionData><DurationText :value="duration" /></DefinitionData>
          </DefinitionList>
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
import { DefinitionList, DefinitionData, DefinitionTerm } from '@/components/Common/DefinitionList';
import { Block, PageHeading, PageSection, DurationText } from '@/components/Common';
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
const duration = computed(() => eventGames.value.reduce((totalTime, game) => totalTime + game.duration, 0));
const location = computed(() => (eventGames.value.length > 0 ? eventGames.value[0].location.name : 'n/a'));
const ready = computed(() => bunchReady.value && eventsReady.value && eventGamesReady.value);
</script>
