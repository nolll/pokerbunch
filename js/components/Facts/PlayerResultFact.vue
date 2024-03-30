<template>
  <DefinitionData>{{ name }}: <WinningsText :show-currency="true" :value="amount" :localization="localization" /></DefinitionData>
</template>

<script setup lang="ts">
import { DefinitionData } from '@/components/Common/DefinitionList';
import { WinningsText } from '@/components/Common';
import { Localization } from '@/models/Localization';

defineProps<{
  name: string;
  amount: number;
  localization: Localization;
}>();
</script>
