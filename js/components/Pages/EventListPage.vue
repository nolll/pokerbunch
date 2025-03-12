<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <template v-slot:aside1>
          <Block>
            <CustomButton :url="addEventUrl" type="action" text="Add event" />
          </Block>
        </template>

        <template v-slot:default>
          <Block>
            <PageHeading text="Events" />
          </Block>
          <Block>
            <EventList :events="events" />
          </Block>
        </template>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import EventList from '@/components/EventList/EventList.vue';
import { Block, CustomButton, PageHeading, PageSection } from '@/components/Common';
import urls from '@/urls';
import { computed } from 'vue';
import { useParams, useEventList } from '@/composables';

const { slug } = useParams();
const { events, eventsReady } = useEventList(slug.value);

const addEventUrl = computed(() => urls.event.add(slug.value));
const ready = computed(() => eventsReady.value);
</script>
