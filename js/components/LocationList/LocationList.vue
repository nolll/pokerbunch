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
import { computed } from 'vue';

const props = defineProps<{
  locations: LocationResponse[];
}>();

const bunches = useBunches();

const bunchId = computed(() => {
  return bunches.slug.value;
});

const locationList = computed(() => {
  return props.locations.slice().sort(compareLocation);
});

const compareLocation = (a: LocationResponse, b: LocationResponse) => {
  return comparer.compare(a.name, b.name);
};
</script>
