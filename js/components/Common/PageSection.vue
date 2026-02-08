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

const props = withDefaults(
  defineProps<{
    isWide?: boolean;
  }>(),
  {
    isWide: false,
  },
);

const slots = useSlots();

const isAside1Enabled = computed(() => isSlotEnabled('aside1'));
const isAside2Enabled = computed(() => isSlotEnabled('aside2'));
const hasAside = computed(() => isAside1Enabled.value || isAside2Enabled.value);

const cssClasses = computed((): CssClasses => {
  return {
    'page-section': true,
    'page-section--wide': props.isWide,
  };
});

const asideCssClasses = computed(
  (): CssClasses => ({
    region: true,
    aside: true,
  }),
);

const mainCssClasses = computed(
  (): CssClasses => ({
    region: true,
    width2: hasAside.value,
  }),
);

const isSlotEnabled = (name: string) => !!slots[name];
</script>

<style lang="scss" scoped>
@use '../../../css/mediaqueries';

.page-section {
  max-width: 981px;
  margin: 0 auto;
  padding: 0 15px;
}

.page-section--wide {
  max-width: none;
  margin: 0 auto;
}

@include mediaqueries.widthSmall {
  .page-section {
    display: flex;
    justify-content: space-between;
  }
  .region {
    flex: 1;
    width: 32%;
  }
  .region.width2 {
    flex: 2;
    order: 1;
  }
  .region.aside {
    padding-left: 15px;
    order: 2;
  }
}
</style>
