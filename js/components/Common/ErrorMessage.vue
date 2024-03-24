<template>
  <div class="errors" v-if="isVisible">
    <p class="validation-error">
      {{ message }}
    </p>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue';

const props = defineProps<{
  message: string | null | undefined;
}>();

const isVisible = computed(() => Boolean(props.message));
</script>
