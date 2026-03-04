<template>
  <button class="button button--icon">
    <div class="button__icon">
      <slot></slot>
    </div>
  </button>
</template>

<script setup lang="ts">
defineProps<{
  text: string;
}>();
</script>
