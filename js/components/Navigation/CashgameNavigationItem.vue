<template>
  <li class="cashgame-navigation__item">
    <a :href="url" :class="selectedClass" @click.prevent="click">{{ text }}</a>
  </li>
</template>

<script setup lang="ts">
import { CssClasses } from '@/models/CssClasses';
import { computed } from 'vue';

const props = defineProps<{
  url: string;
  text: string;
  isSelected: boolean;
}>();

const emit = defineEmits(['selected']);

const selectedClass = computed((): CssClasses => ({
  selected: props.isSelected,
}));

const click = () => {
  emit('selected', props.url);
};
</script>

<style lang="scss">
.cashgame-navigation__item {
  margin-top: 5px;
}
</style>
