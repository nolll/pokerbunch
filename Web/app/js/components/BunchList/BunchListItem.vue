<template>
  <div>
    <CustomLink :url="url">{{ name }}</CustomLink>
  </div>
</template>

<script setup lang="ts">
import urls from '@/urls';
import CustomLink from '@/components/Common/CustomLink.vue';
import { computed } from 'vue';

const props = defineProps<{
  id: string;
  name: string;
}>();

const url = computed(() => {
  return urls.bunch.details(props.id);
});
</script>
