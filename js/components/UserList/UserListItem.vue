<template>
  <div>
    <CustomLink :url="url">{{ userName }}</CustomLink>
  </div>
</template>

<script setup lang="ts">
import urls from '@/urls';
import { CustomLink } from '@/components/Common';
import { computed } from 'vue';

const props = defineProps<{
  userName: string;
}>();

const url = computed(() => urls.user.details(props.userName));
</script>
