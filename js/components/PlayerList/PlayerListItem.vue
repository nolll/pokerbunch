<template>
  <div>
    <span class="player-color-box" :style="colorBoxStyle"></span>
    <CustomLink :url="url">{{ name }}</CustomLink>
  </div>
</template>

<script setup lang="ts">
import urls from '@/urls';
import { CustomLink } from '@/components/Common';
import { computed } from 'vue';

const props = defineProps<{
  bunchId: string;
  id: string;
  name: string;
  color: string;
}>();

const url = computed(() => {
  return urls.player.details(props.bunchId, props.id);
});

const colorBoxStyle = computed(() => {
  return `background-color: ${props.color}`;
});
</script>
