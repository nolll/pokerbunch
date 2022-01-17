<template>
  <select :value="modelValue" v-on:input="updateValue">
    <option value="">Select layout</option>
    <option v-for="currencyLayout in currencyLayouts" :value="currencyLayout" v-bind:key="currencyLayout">
      {{ getDisplayName(currencyLayout) }}
    </option>
  </select>
</template>

<script setup lang="ts">
import { computed } from 'vue';

const props = defineProps<{
  modelValue: string;
  symbol: string;
}>();

const emit = defineEmits(['update:modelValue']);

const currencyLayouts = computed(() => {
  return ['{SYMBOL} {AMOUNT}', '{SYMBOL}{AMOUNT}', '{AMOUNT}{SYMBOL}', '{AMOUNT} {SYMBOL}'];
});

const getDisplayName = (layout: string) => {
  return layout.replace('{SYMBOL}', props.symbol).replace('{AMOUNT}', '123');
};

const updateValue = (event: Event) => {
  const value = (event.target as HTMLInputElement).value;
  emit('update:modelValue', value);
};
</script>
