<template>
  <div>
    <CustomLink :url="url">{{ name }}</CustomLink
    >,
    {{ details }}
  </div>
</template>

<script setup lang="ts">
import urls from '@/urls';
import { CustomLink } from '@/components/Common';
import { EventResponse } from '@/response/EventResponse';
import { computed } from 'vue';

const props = defineProps<{
  event: EventResponse;
}>();

const name = computed(() => {
  return props.event.name;
});

const location = computed(() => {
  return props.event.location.name;
});

const date = computed(() => {
  return props.event.startDate;
});

const url = computed(() => {
  return urls.event.details(props.event.bunchId, props.event.id.toString());
});

const details = computed(() => {
  return hasGames.value ? `${location.value}, ${date.value}` : 'No games';
});

const hasGames = computed(() => {
  return !!props.event.location && !!props.event.startDate;
});
</script>
