<template>
  <CustomLink :url="url">{{ name }}</CustomLink>
</template>

<script setup lang="ts">
import urls from '@/urls';
import { CustomLink } from '@/components/Common';
import { computed } from 'vue';

const props = defineProps<{
  id: string;
  name: string;
}>();

const url = computed(() => {
  return urls.bunch.details(props.id);
});
</script>
