<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <PageHeading text="Add Event" />
        </Block>

        <Block>
          <div class="field">
            <label class="label" for="event-name">Name</label>
            <input class="textfield" v-model="eventName" id="event-name" type="text" />
          </div>
          <div class="buttons">
            <CustomButton v-on:click="add" type="action" text="Add" />
            <CustomButton v-on:click="cancel" text="Cancel" />
          </div>
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import Block from '@/components/Common/Block.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import urls from '@/urls';
import useBunches from '@/composables/useBunches';
import useEvents from '@/composables/useEvents';
import { computed, onMounted, ref, watch } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const bunches = useBunches();
const events = useEvents();

const eventName = ref('');

const init = () => {
  bunches.loadBunch();
  events.loadEvents();
};

const add = () => {
  if (eventName.value.length > 0) {
    events.addEvent(eventName.value);
    redirect();
  }
};

const cancel = () => {
  redirect();
};

const redirect = () => {
  router.push(urls.event.list(bunches.slug.value));
};

const ready = computed(() => {
  return bunches.bunchReady.value && events.eventsReady.value;
});

onMounted(() => {
  init();
});
</script>
