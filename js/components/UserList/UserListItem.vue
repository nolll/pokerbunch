<template>
  <div>
    <CustomLink :url="url">{{ userName }}</CustomLink>
  </div>
</template>

<script setup lang="ts">
import urls from '@/urls';
import CustomLink from '@/components/Common/CustomLink.vue';
import { computed } from 'vue';

const props = defineProps<{
  userName: string;
}>();

const url = computed(() => {
  return urls.user.details(props.userName);
});
</script>
