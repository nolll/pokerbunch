<template>
  <DefinitionData>{{ name }}: <CurrencyText :value="amount" :localization="localization" /></DefinitionData>
</template>

<script setup lang="ts">
import { DefinitionData } from '@/components/Common/DefinitionList';
import { CurrencyText } from '@/components/Common';
import { Localization } from '@/models/Localization';

defineProps<{
  name: string;
  amount: number;
  localization: Localization;
}>();
</script>
