<template>
  <DefinitionData>{{ name }}: <DurationText :value="minutes" /></DefinitionData>
</template>

<script setup lang="ts">
import { DefinitionData } from '@/components/Common/DefinitionList';
import { DurationText } from '@/components/Common';

defineProps<{
  name: string;
  minutes: number;
}>();
</script>
