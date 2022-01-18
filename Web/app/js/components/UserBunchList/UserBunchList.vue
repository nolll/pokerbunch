<template>
  <div v-if="ready && hasBunches">
    <h1 class="module-heading">Your Bunches</h1>
    <SimpleList>
      <SimpleListItem v-for="bunch in userBunches" :key="bunch.id">
        <UserBunchList-item :id="bunch.id" :name="bunch.name" />
      </SimpleListItem>
    </SimpleList>
  </div>
</template>

<script setup lang="ts">
import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
import useBunches from '@/composables/useBunches';
import { computed } from 'vue';

const bunches = useBunches();

const userBunches = computed(() => {
  return bunches.userBunches.value;
});

const hasBunches = computed(() => {
  return bunches.userBunches.value.length > 0;
});

const ready = computed(() => {
  return bunches.userBunchesReady.value;
});
</script>
