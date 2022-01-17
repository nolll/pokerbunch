<template>
  <select :value="modelValue" v-on:input="updateValue">
    <option value="">All years</option>
    <option v-for="year in years" :value="year" v-bind:key="year">
      {{ year }}
    </option>
  </select>
</template>

<script setup lang="ts">
defineProps<{
  modelValue?: number;
  years: number[];
}>();

const emit = defineEmits(['update:modelValue']);

const updateValue = (event: Event) => {
  const value = (event.target as HTMLInputElement).value;
  emit('update:modelValue', value);
};
</script>
