<template>
  <select :value="selectedPlayerId" v-on:input="updateValue">
    <option v-for="player in players" :value="player.id" v-bind:key="player.id">{{ player.name }}</option>
  </select>
</template>

<script setup lang="ts">
import { Player } from '@/models/Player';
import { computed } from 'vue';

const props = defineProps<{
  modelValue: string;
  defaultPlayerId: string;
  players: Player[];
}>();

const selectedPlayerId = computed(() => {
  return props.modelValue.length ? props.modelValue : props.defaultPlayerId;
});

const emit = defineEmits(['update:modelValue']);

const updateValue = (event: Event) => {
  const value = (event.target as HTMLInputElement).value;
  emit('update:modelValue', value);
};
</script>
