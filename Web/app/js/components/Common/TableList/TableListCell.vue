<template>
  <td class="table-list__cell" :class="cssClasses">
    <slot></slot>
  </td>
</template>

<script setup lang="ts">
import { CssClasses } from '@/models/CssClasses';
import { computed } from 'vue';

const props = withDefaults(
  defineProps<{
    isNumeric?: boolean;
    customClasses?: CssClasses;
  }>(),
  {
    isNumeric: false,
    customClasses: () => {
      return {};
    },
  }
);

const cssClasses = computed((): CssClasses => {
  return {
    ...props.customClasses,
    'table-list__cell--numeric': props.isNumeric,
  };
});
</script>
