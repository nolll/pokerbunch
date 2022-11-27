<template>
  <select :value="modelValue" v-on:input="updateValue">
    <option v-for="player in players" :value="player.id" v-bind:key="player.id">{{ player.name }}</option>
  </select>
</template>

<script setup lang="ts">
import { Player } from '@/models/Player';

defineProps<{
  modelValue: string;
  players: Player[];
}>();

const emit = defineEmits(['update:modelValue']);

const updateValue = (event: Event) => {
  const value = (event.target as HTMLInputElement).value;
  emit('update:modelValue', value);
};
</script>
