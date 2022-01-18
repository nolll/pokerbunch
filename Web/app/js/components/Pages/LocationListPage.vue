<template>
  <Layout :ready="ready">
    <template slot="top-nav">
      <BunchNavigation />
    </template>

    <PageSection>
      <template slot="aside1">
        <Block>
          <CustomButton :url="addLocationUrl" type="action" text="Add location" />
        </Block>
      </template>

      <Block>
        <PageHeading text="Locations" />
      </Block>

      <Block>
        <LocationList />
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import LocationList from '@/components/LocationList/LocationList.vue';
import Block from '@/components/Common/Block.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import urls from '@/urls';
import { useRoute } from 'vue-router';
import useLocations from '@/composables/useLocations';
import useBunches from '@/composables/useBunches';
import useUsers from '@/composables/useUsers';
import { computed, onMounted, watch } from 'vue';

const route = useRoute();
const users = useUsers();
const bunches = useBunches();
const locations = useLocations();

const addLocationUrl = computed(() => {
  return urls.location.add(bunches.slug.value);
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

watch(route, () => {
  init();
});
</script>
