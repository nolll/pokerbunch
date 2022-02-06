<template>
  <select :value="modelValue" v-on:input="updateValue">
    <option value="">Select Event</option>
    <option v-for="event in eventList" :value="event.id" v-bind:key="event.id">
      {{ event.name }}
    </option>
  </select>
</template>

<script setup lang="ts">
import useEvents from '@/composables/useEvents';
import { computed } from 'vue';

defineProps<{
  modelValue?: string;
}>();

const emit = defineEmits(['update:modelValue']);

const events = useEvents();

const eventList = computed(() => {
  return events.events.value;
});

const updateValue = (event: Event) => {
  const value = (event.target as HTMLInputElement).value;
  emit('update:modelValue', value);
};
</script>
