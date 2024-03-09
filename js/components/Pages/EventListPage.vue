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
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import EventList from '@/components/EventList/EventList.vue';
import Block from '@/components/Common/Block.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import urls from '@/urls';
import { computed, onMounted } from 'vue';
import useParams from '@/composables/useParams';
import useEventList from '@/composables/useEventList';

const { slug } = useParams();
const { events, eventsReady } = useEventList(slug.value);

const addEventUrl = computed(() => {
  return urls.event.add(slug.value);
});

const ready = computed(() => {
  return eventsReady.value;
});
</script>
