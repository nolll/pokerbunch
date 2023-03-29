<template>
  <select :value="modelValue" v-on:input="updateValue">
    <option value="">Select Event</option>
    <option v-for="event in eventList" :value="event.id" v-bind:key="event.id">
      {{ event.name }}
    </option>
  </select>
</template>

<script setup lang="ts">
import { EventResponse } from '@/response/EventResponse';
import { computed } from 'vue';

const props = defineProps<{
  modelValue?: string;
  events: EventResponse[];
}>();

const emit = defineEmits(['update:modelValue']);

const eventList = computed(() => {
  return props.events;
});

const updateValue = (event: Event) => {
  const value = (event.target as HTMLInputElement).value;
  emit('update:modelValue', value);
};
</script>
