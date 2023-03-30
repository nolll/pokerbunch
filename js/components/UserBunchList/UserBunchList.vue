<template>
  <div v-if="hasBunches">
    <h1 class="module-heading">Your Bunches</h1>
    <SimpleList>
      <SimpleListItem v-for="bunch in bunches" :key="bunch.id">
        <UserBunchListItem :id="bunch.id" :name="bunch.name" />
      </SimpleListItem>
    </SimpleList>
  </div>
</template>

<script setup lang="ts">
import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
import { BunchResponse } from '@/response/BunchResponse';
import { computed } from 'vue';
import UserBunchListItem from './UserBunchListItem.vue';

const props = defineProps<{
  bunches: BunchResponse[];
}>();

const hasBunches = computed(() => {
  return props.bunches.length > 0;
});
</script>
