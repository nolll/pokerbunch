<template>
  <div v-if="hasBunches">
    <h1 class="module-heading">Your Bunches</h1>
    <SimpleList>
      <SimpleListItem v-for="bunch in userBunches" :key="bunch.id">
        <UserBunchListItem :id="bunch.id" :name="bunch.name" />
      </SimpleListItem>
    </SimpleList>
  </div>
</template>

<script setup lang="ts">
import { SimpleList, SimpleListItem } from '@/components/Common/SimpleList';
import { computed } from 'vue';
import UserBunchListItem from './UserBunchListItem.vue';
import { BunchResponse } from '@/response/BunchResponse';

const props = defineProps<{
  bunches: BunchResponse[];
}>();

const userBunches = computed(() => props.bunches);
const hasBunches = computed(() => props.bunches.length > 0);
</script>
