<template>
  <SimpleList>
    <SimpleListItem v-for="event in eventList" :key="event.id">
      <EventListItem :event="event" />
    </SimpleListItem>
  </SimpleList>
</template>

<script setup lang="ts">
import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
import EventListItem from '@/components/EventList/EventListItem.vue';
import useEvents from '@/composables/useEvents';
import { computed } from 'vue';

const events = useEvents();

const eventList = computed(() => {
  return events.events.value;
});

const ready = computed(() => {
  return events.eventsReady;
});
</script>
