<template>
  <span :class="cssClasses">{{ formattedValue }}</span>
</template>

<script setup lang="ts">
import useFormatter from '@/composables/useFormatter';
import { CssClasses } from '@/models/CssClasses';
import { computed } from 'vue';

const props = withDefaults(
  defineProps<{
    value: number;
    showCurrency?: boolean;
  }>(),
  {
    showCurrency: false,
  }
);

const formatter = useFormatter();

const formattedValue = computed(() => {
  if (props.showCurrency) return formatter.formatResult(props.value);
  return formatter.formatResultWithoutCurrency(props.value);
});

const cssClasses = computed((): CssClasses => {
  return {
    'pos-result': props.value > 0,
    'neg-result': props.value < 0,
  };
});
</script>
