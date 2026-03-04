<template>
  <v-icon :name="icon" :scale="scale" :title="title" />
</template>

<script setup lang="ts">
import { computed } from 'vue';

const props = withDefaults(
  defineProps<{
    name: 'buyin' | 'report' | 'cashout' | 'checkmark' | 'time' | 'delete';
    scale?: number;
    title?: string;
  }>(),
  {
    scale: 1,
  },
);

const icon = computed(() => {
  if (props.name === 'buyin') return 'io-arrow-forward-circle-outline';
  if (props.name === 'report') return 'io-menu-outline';
  if (props.name === 'cashout') return 'io-cash-outline';
  if (props.name === 'checkmark') return 'io-checkmark-outline';
  if (props.name === 'time') return 'io-time-outline';
  if (props.name === 'delete') return 'io-trash-outline';
});
</script>
