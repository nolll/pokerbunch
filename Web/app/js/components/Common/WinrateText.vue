<template>
  <span :class="cssClasses">{{ formattedValue }}</span>
</template>

<script setup lang="ts">
import useFormatter from '@/composables/useFormatter';
import { CssClasses } from '@/models/CssClasses';
import { computed } from 'vue';

const props = defineProps<{
  value: number;
}>();

const formatter = useFormatter();

const formattedValue = computed(() => {
  return formatter.formatWinrate(props.value);
});

const cssClasses = computed((): CssClasses => {
  return {
    'pos-result': props.value > 0,
    'neg-result': props.value < 0,
  };
});
</script>
