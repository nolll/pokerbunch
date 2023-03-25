<template>
  <span>{{ formattedValue }}</span>
</template>

<script setup lang="ts">
import format from '@/format';
import { computed } from 'vue';

const props = defineProps<{
  value: number;
}>();

const formattedValue = computed(() => {
  return format.duration(props.value);
});
</script>
