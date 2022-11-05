<template>
  <button class="button button--icon">
    <div class="button__icon">
      <slot></slot>
    </div>
    {{ text }}
  </button>
</template>

<script setup lang="ts">
defineProps<{
  text: string;
}>();
</script>
