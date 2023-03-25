<template>
  <span>{{ formattedValue }}</span>
</template>

<script setup lang="ts">
import format from '@/format';
import { bunchKey } from '@/helpers/injectionKeys';
import { computed, inject } from 'vue';

const props = defineProps<{
  value: number;
}>();

const bunch = inject(bunchKey);

const formattedValue = computed(() => {
  return format.currency(props.value, bunch?.value?.currencyFormat, bunch?.value?.thousandSeparator);
});
</script>
