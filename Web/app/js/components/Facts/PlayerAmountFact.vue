<template>
  <DefinitionData>{{ name }}: <CurrencyText :value="amount" /></DefinitionData>
</template>

<script setup lang="ts">
import DefinitionData from '@/components/DefinitionList/DefinitionData.vue';
import CurrencyText from '@/components/Common/CurrencyText.vue';

defineProps<{
  name: string;
  amount: number;
}>();
</script>
