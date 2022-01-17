<template>
  <button class="button button--icon">
    <i :class="iconCssClass"></i>
    {{ text }}
  </button>
</template>

<script setup lang="ts">
import { computed } from 'vue';

const props = defineProps<{
  text: string;
  icon: string;
}>();

const iconCssClass = computed(() => {
  return `icon-${props.icon}`;
});
</script>
