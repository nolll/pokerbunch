<template>
  <span :class="cssClasses">{{ formattedValue }}</span>
</template>

<script setup lang="ts">
import format from '@/format';
import { bunchKey } from '@/helpers/injectionKeys';
import { CssClasses } from '@/models/CssClasses';
import { computed, inject } from 'vue';

const props = defineProps<{
  value: number;
}>();

const bunch = inject(bunchKey);

const formattedValue = computed(() => {
  return format.winrate(props.value, bunch?.value.currencyFormat, bunch?.value.thousandSeparator);
});

const cssClasses = computed((): CssClasses => {
  return {
    'pos-result': props.value > 0,
    'neg-result': props.value < 0,
  };
});
</script>
