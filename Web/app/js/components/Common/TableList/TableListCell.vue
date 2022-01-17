<template>
  <td class="table-list__cell" :class="cssClasses">
    <slot></slot>
  </td>
</template>

<script setup lang="ts">
import { CssClasses } from '@/models/CssClasses';
import { computed } from 'vue';

const props = defineProps<{
  isNumeric: boolean;
  customClasses: CssClasses;
}>();

const cssClasses = computed((): CssClasses => {
  return {
    ...props.customClasses,
    'table-list__cell--numeric': props.isNumeric,
  };
});
</script>
