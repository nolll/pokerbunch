<template>
  <DefinitionData>{{ name }}: <WinningsText :show-currency="true" :value="amount" :localization="localization" /></DefinitionData>
</template>

<script setup lang="ts">
import DefinitionData from '@/components/DefinitionList/DefinitionData.vue';
import WinningsText from '@/components/Common/WinningsText.vue';
import { Localization } from '@/models/Localization';

defineProps<{
  name: string;
  amount: number;
  localization: Localization;
}>();
</script>
