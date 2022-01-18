<template>
  <SimpleList>
    <SimpleListItem v-for="player in playerList" :key="player.id">
      <PlayerList-item :id="player.id" :name="player.name" :color="player.color" :bunchId="bunchId" />
    </SimpleListItem>
  </SimpleList>
</template>

<script setup lang="ts">
import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
import usePlayers from '@/composables/usePlayers';
import { computed } from 'vue';

defineProps<{
  bunchId: string;
}>();

const players = usePlayers();

const playerList = computed(() => {
  return players.players.value;
});

const ready = computed(() => {
  return players.playersReady.value;
});
</script>
