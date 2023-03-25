<template>
  <span :class="cssClasses">{{ formattedValue }}</span>
</template>

<script setup lang="ts">
import format from '@/format';
import { bunchKey } from '@/helpers/injectionKeys';
import { CssClasses } from '@/models/CssClasses';
import { computed, inject } from 'vue';

const props = withDefaults(
  defineProps<{
    value: number;
    showCurrency?: boolean;
  }>(),
  {
    showCurrency: false,
  }
);

const bunch = inject(bunchKey);

const formattedValue = computed(() => {
  if (props.showCurrency) return format.result(props.value, bunch?.value.currencyFormat, bunch?.value.thousandSeparator);
  return format.resultWithoutCurrency(props.value);
});

const cssClasses = computed((): CssClasses => {
  return {
    'pos-result': props.value > 0,
    'neg-result': props.value < 0,
  };
});
</script>
