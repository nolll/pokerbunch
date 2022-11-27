<template>
  <CustomLink :url="url">{{ name }}</CustomLink>
</template>

<script setup lang="ts">
import urls from '@/urls';
import CustomLink from '@/components/Common/CustomLink.vue';
import { LocationResponse } from '@/response/LocationResponse';
import { computed } from 'vue';

const props = defineProps<{
  location: LocationResponse;
  bunchId: string;
}>();

const name = computed(() => {
  return props.location.name;
});

const url = computed(() => {
  return urls.location.details(props.bunchId, props.location.id.toString());
});
</script>
