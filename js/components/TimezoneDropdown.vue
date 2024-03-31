<template>
  <select :value="modelValue" v-on:input="updateValue">
    <option value="">Select timezone</option>
    <option v-for="timezone in timezoneList" :value="timezone.id" v-bind:key="timezone.id">
      {{ timezone.name }}
    </option>
  </select>
</template>

<script setup lang="ts">
import { useTimezones } from '@/composables';
import { computed } from 'vue';

defineProps<{
  modelValue?: string;
}>();

const emit = defineEmits(['update:modelValue']);

const timezones = useTimezones();

const timezoneList = computed(() => {
  return timezones.getTimezones();
});

const updateValue = (event: Event) => {
  const value = (event.target as HTMLInputElement).value;
  emit('update:modelValue', value);
};
</script>
