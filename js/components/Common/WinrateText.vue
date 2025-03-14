<template>
  <span :class="cssClasses">{{ formattedValue }}</span>
</template>

<script setup lang="ts">
import format from '@/format';
import { CssClasses } from '@/models/CssClasses';
import { Localization } from '@/models/Localization';
import { computed } from 'vue';

const props = defineProps<{
  value: number;
  localization: Localization;
}>();

const formattedValue = computed(() => format.winrate(props.value, props.localization));

const cssClasses = computed((): CssClasses => ({
  'pos-result': props.value > 0,
  'neg-result': props.value < 0,
}));
</script>
