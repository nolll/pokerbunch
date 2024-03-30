<template>
  <SimpleList>
    <SimpleListItem v-for="bunch in bunches" :key="bunch.id">
      <BunchListItem :id="bunch.id" :name="bunch.name" />
    </SimpleListItem>
  </SimpleList>
</template>

<script setup lang="ts">
import { SimpleList, SimpleListItem } from '@/components/Common/SimpleList';
import BunchListItem from '@/components/BunchList/BunchListItem.vue';
import { BunchResponse } from '@/response/BunchResponse';

defineProps<{
  bunches: BunchResponse[];
}>();
</script>
