<template>
  <span :class="cssClasses">{{ formattedValue }}</span>
</template>

<script setup lang="ts">
import format from '@/format';
import { CssClasses } from '@/models/CssClasses';
import { Localization } from '@/models/Localization';
import { computed } from 'vue';

const props = withDefaults(
  defineProps<{
    value: number;
    showCurrency?: boolean;
    localization?: Localization;
  }>(),
  {
    showCurrency: false,
  }
);

const formattedValue = computed(() => {
  if (props.showCurrency && props.localization) return format.result(props.value, props.localization);
  return format.resultWithoutCurrency(props.value);
});

const cssClasses = computed((): CssClasses => ({
  'pos-result': props.value > 0,
  'neg-result': props.value < 0,
}));
</script>
