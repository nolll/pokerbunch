<template>
  <SimpleList>
    <SimpleListItem v-for="bunch in bunches" :key="bunch.id">
      <BunchListItem :id="bunch.id" :name="bunch.name" />
    </SimpleListItem>
  </SimpleList>
</template>

<script setup lang="ts">
import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
import BunchListItem from '@/components/BunchList/BunchListItem.vue';
import { BunchResponse } from '@/response/BunchResponse';

defineProps<{
  bunches: BunchResponse[];
}>();
</script>
