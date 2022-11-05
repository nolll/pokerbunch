<template>
  <DefinitionData>{{ name }}: <WinningsText :value="amount" /></DefinitionData>
</template>

<script setup lang="ts">
import DefinitionData from '@/components/DefinitionList/DefinitionData.vue';
import WinningsText from '@/components/Common/WinningsText.vue';

defineProps<{
  name: string;
  amount: number;
}>();
</script>
