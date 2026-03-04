<template>
  <table :class="cssClasses">
    <slot></slot>
  </table>
</template>

<script setup lang="ts">
import { CssClasses } from '@/models/CssClasses';
import { computed } from 'vue';

const props = withDefaults(
  defineProps<{
    isWide?: boolean;
  }>(),
  {
    isWide: false,
  },
);

const cssClasses = computed((): CssClasses => {
  return {
    'table-list': true,
    'table-list--wide': props.isWide,
  };
});
</script>

<style lang="scss" scoped>
.table-list {
  margin-bottom: 10px;
}
.table-list--wide {
  width: 100%;
}
</style>
