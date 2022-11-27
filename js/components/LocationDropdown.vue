<template>
  <select :value="modelValue" v-on:input="updateValue">
    <option value="">Select Location</option>
    <option v-for="location in locationList" :value="location.id" v-bind:key="location.id">
      {{ location.name }}
    </option>
  </select>
</template>

<script setup lang="ts">
import useLocations from '@/composables/useLocations';
import { computed } from 'vue';

defineProps<{
  modelValue?: string;
}>();

const emit = defineEmits(['update:modelValue']);

const locations = useLocations();

const locationList = computed(() => {
  return locations.locations.value;
});

const updateValue = (event: Event) => {
  const value = (event.target as HTMLInputElement).value;
  emit('update:modelValue', value);
};
</script>
