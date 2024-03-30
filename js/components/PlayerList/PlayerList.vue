<template>
  <SimpleList>
    <SimpleListItem v-for="player in players" :key="player.id">
      <PlayerListItem :id="player.id" :name="player.name" :color="player.color" :bunchId="bunchId" />
    </SimpleListItem>
  </SimpleList>
</template>

<script setup lang="ts">
import { SimpleList, SimpleListItem } from '@/components/Common/SimpleList';
import PlayerListItem from './PlayerListItem.vue';
import { Player } from '@/models/Player';

defineProps<{
  bunchId: string;
  players: Player[];
}>();
</script>
