<template>
  <Layout :ready="ready">
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
            <EventList />
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
import useUsers from '@/composables/useUsers';
import useBunches from '@/composables/useBunches';
import useEvents from '@/composables/useEvents';
import { computed, onMounted } from 'vue';

const users = useUsers();
const bunches = useBunches();
const events = useEvents();

const addEventUrl = computed(() => {
  return urls.event.add(bunches.slug.value);
});

const ready = computed(() => {
  return bunches.bunchReady.value && events.eventsReady.value;
});

const init = () => {
  users.requireUser();
  bunches.loadBunch();
  events.loadEvents();
};

onMounted(() => {
  init();
});
</script>
