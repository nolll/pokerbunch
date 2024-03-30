<template>
  <SimpleList>
    <SimpleListItem v-for="event in events" :key="event.id">
      <EventListItem :event="event" />
    </SimpleListItem>
  </SimpleList>
</template>

<script setup lang="ts">
import { SimpleList, SimpleListItem } from '@/components/Common/SimpleList';
import EventListItem from '@/components/EventList/EventListItem.vue';
import { EventResponse } from '@/response/EventResponse';

defineProps<{
  events: EventResponse[];
}>();
</script>
