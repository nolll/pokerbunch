<template>
  <h1 class="page-heading">{{ text }}</h1>
</template>

<script setup lang="ts">
defineProps<{
  text: string;
}>();
</script>
