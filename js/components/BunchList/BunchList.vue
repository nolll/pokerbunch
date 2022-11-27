<template>
  <SimpleList>
    <SimpleListItem v-for="bunch in bunchList" :key="bunch.id">
      <BunchListItem :id="bunch.id" :name="bunch.name" />
    </SimpleListItem>
  </SimpleList>
</template>

<script setup lang="ts">
import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
import BunchListItem from '@/components/BunchList/BunchListItem.vue';
import useBunches from '@/composables/useBunches';
import { computed } from 'vue';

const bunches = useBunches();

const bunchList = computed(() => {
  return bunches.bunches.value;
});
</script>
