<template>
  <div :class="cssClasses">
    <div v-if="isAside1Enabled" :class="asideCssClasses">
      <slot name="aside1"></slot>
    </div>
    <div :class="mainCssClasses">
      <slot></slot>
    </div>
    <div v-if="isAside2Enabled" :class="asideCssClasses">
      <slot name="aside2"></slot>
    </div>
  </div>
</template>

<script setup lang="ts">
import { CssClasses } from '@/models/CssClasses';
import { computed, useSlots } from 'vue';

const props = defineProps<{
  isWide: boolean;
}>();

const slots = useSlots();

const isAside1Enabled = computed(() => {
  return isSlotEnabled('aside1');
});

const isAside2Enabled = computed(() => {
  return isSlotEnabled('aside2');
});

const hasAside = computed(() => {
  return isAside1Enabled.value || isAside2Enabled.value;
});

const cssClasses = computed((): CssClasses => {
  return {
    'page-section': true,
    'page-section--wide': props.isWide,
  };
});

const asideCssClasses = computed((): CssClasses => {
  return {
    region: true,
    aside: true,
  };
});

const mainCssClasses = computed((): CssClasses => {
  return {
    region: true,
    width2: hasAside.value,
  };
});

const isSlotEnabled = (name: string) => {
  return !!slots[name];
};
</script>
