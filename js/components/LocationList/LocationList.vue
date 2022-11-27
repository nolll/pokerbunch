<template>
  <SimpleList>
    <SimpleListItem v-for="location in locationList" :key="location.id">
      <LocationListItem :bunch-id="bunchId" :location="location" />
    </SimpleListItem>
  </SimpleList>
</template>

<script setup lang="ts">
import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
import LocationListItem from '@/components/LocationList/LocationListItem.vue';
import comparer from '@/comparer';
import { LocationResponse } from '@/response/LocationResponse';
import useBunches from '@/composables/useBunches';
import useLocations from '@/composables/useLocations';
import { computed } from 'vue';

const bunches = useBunches();
const locations = useLocations();

const bunchId = computed(() => {
  return bunches.slug.value;
});

const locationList = computed(() => {
  return locations.locations.value.slice().sort(compareBuyin);
});

const ready = computed(() => {
  return locations.locationsReady.value;
});

const compareBuyin = (a: LocationResponse, b: LocationResponse) => {
  return comparer.compare(a.name, b.name);
};
</script>
