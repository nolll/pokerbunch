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
import useUsers from '@/composables/useUsers';
import useBunches from '@/composables/useBunches';
import useLocations from '@/composables/useLocations';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();
const users = useUsers();
const bunches = useBunches();
const locations = useLocations();

const name = computed(() => {
  if (location.value) return location.value.name;
  return '';
});

const location = computed(() => {
  for (let i = 0; i < locations.locations.value.length; i++) {
    const location = locations.locations.value[i];
    if (location.id.toString() === locationId.value) return location;
  }
  return null;
});

const locationId = computed(() => {
  return route.params.id as string;
});

const ready = computed(() => {
  return bunches.bunchReady.value && locations.locationsReady.value;
});

const init = () => {
  users.requireUser();
  bunches.loadBunch();
  locations.loadLocations();
};

onMounted(() => {
  init();
});
</script>
