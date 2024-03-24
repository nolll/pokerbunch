<template>
  <SimpleList>
    <SimpleListItem v-for="location in locationList" :key="location.id">
      <LocationListItem :bunch-id="slug" :location="location" />
    </SimpleListItem>
  </SimpleList>
</template>

<script setup lang="ts">
import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
import LocationListItem from '@/components/LocationList/LocationListItem.vue';
import comparer from '@/comparer';
import { LocationResponse } from '@/response/LocationResponse';
import { computed } from 'vue';

const props = defineProps<{
  slug: string;
  locations: LocationResponse[];
}>();

const locationList = computed(() => {
  return props.locations.slice().sort(compareLocation);
});

const compareLocation = (a: LocationResponse, b: LocationResponse) => {
  return comparer.compare(a.name, b.name);
};
</script>
