<template>
  <select :value="modelValue" v-on:input="updateValue">
    <option value="">Select Location</option>
    <option v-for="location in locations" :value="location.id" v-bind:key="location.id">
      {{ location.name }}
    </option>
  </select>
</template>

<script setup lang="ts">
import { LocationResponse } from '@/response/LocationResponse';

defineProps<{
  modelValue?: string;
  locations: LocationResponse[];
}>();

const emit = defineEmits(['update:modelValue']);

const updateValue = (event: Event) => {
  const value = (event.target as HTMLInputElement).value;
  emit('update:modelValue', value);
};
</script>
