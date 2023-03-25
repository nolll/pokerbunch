<template>
  <SimpleList>
    <SimpleListItem v-for="player in players" :key="player.id">
      <PlayerListItem :id="player.id" :name="player.name" :color="player.color" :bunchId="slug" />
    </SimpleListItem>
  </SimpleList>
</template>

<script setup lang="ts">
import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
import { PlayerResponse } from '@/response/PlayerResponse';
import PlayerListItem from './PlayerListItem.vue';

defineProps<{
  slug: string;
  players: PlayerResponse[];
}>();
</script>
