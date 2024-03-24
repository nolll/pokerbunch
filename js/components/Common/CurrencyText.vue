<template>
  <span>{{ formattedValue }}</span>
</template>

<script setup lang="ts">
import format from '@/format';
import { Localization } from '@/models/Localization';
import { computed } from 'vue';

const props = defineProps<{
  value: number;
  localization: Localization;
}>();

const formattedValue = computed(() => {
  return format.currency(props.value, props.localization);
});
</script>
