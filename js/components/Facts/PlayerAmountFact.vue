<template>
  <DefinitionData>{{ name }}: <CurrencyText :value="amount" :localization="localization" /></DefinitionData>
</template>

<script setup lang="ts">
import DefinitionData from '@/components/DefinitionList/DefinitionData.vue';
import CurrencyText from '@/components/Common/CurrencyText.vue';
import { Localization } from '@/models/Localization';

defineProps<{
  name: string;
  amount: number;
  localization: Localization;
}>();
</script>
