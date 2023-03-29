<template>
  <Layout :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <PageHeading :text="name" />
        </Block>
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
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import MatrixTable from '@/components/Matrix/MatrixTable.vue';
import { computed, onMounted, provide, ref } from 'vue';
import useParams from '@/helpers/useParams';
import { useEventsQuery } from '@/queries/eventQueries';
import { useEventGamesQuery } from '@/queries/gameArchiveQueries';
import auth from '@/auth';
import { useBunchQuery } from '@/queries/bunchQueries';
import { bunchKey } from '@/helpers/injectionKeys';

const params = useParams();
const eventsQuery = useEventsQuery(params.slug.value);
const eventId = computed(() => params.id.value);
const eventGamesQuery = useEventGamesQuery(params.slug.value, eventId.value);
const bunchQuery = useBunchQuery(params.slug.value);
const bunch = computed(() => bunchQuery.data.value!);
const name = computed(() => (event.value ? event.value.name : ''));
const slug = computed(() => params.slug.value);
const event = computed(() => events.value.find((o) => o.id === eventId.value));
const events = computed(() => eventsQuery.data.value ?? []);
const games = computed(() => eventGamesQuery.data.value ?? []);

const ready = computed(() => {
  return bunchQuery.isSuccess.value && eventsQuery.isSuccess.value && eventGamesQuery.isSuccess.value;
});

provide(bunchKey, bunch);

onMounted(async () => auth.requireUser());
</script>
