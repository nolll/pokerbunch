<template>
  <DefinitionData>{{ name }}: <DurationText :value="minutes" /></DefinitionData>
</template>

<script setup lang="ts">
import DefinitionData from '@/components/DefinitionList/DefinitionData.vue';
import DurationText from '@/components/Common/DurationText.vue';

defineProps<{
  name: string;
  minutes: number;
}>();
</script>
