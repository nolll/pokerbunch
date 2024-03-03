<template>
  <div class="top-list">
    <TableList>
      <thead>
        <tr>
          <TableListColumnHeader />
          <TableListColumnHeader>Player</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="winnings">Winnings</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="buyin">Buyin</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="stack">Cashout</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="time">Time</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="gamecount">Games</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="winrate">Win rate</TableListColumnHeader>
        </tr>
      </thead>
      <tbody class="list">
        <TopListRow v-for="player in players" :player="player" :key="player.id" :bunchId="bunchId" />
      </tbody>
    </TableList>
  </div>
</template>

<script setup lang="ts">
import TopListRow from './TopListRow.vue';
import TableList from '@/components/Common/TableList/TableList.vue';
import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';
import useGameArchive from '@/composables/old/useGameArchive';
import { computed } from 'vue';

const gameArchive = useGameArchive();

defineProps<{
  bunchId: string;
}>();

const players = computed(() => {
  return gameArchive.sortedPlayers.value;
});

const orderedBy = computed(() => {
  return gameArchive.playerSortOrder.value;
});

const sort = (column: string) => {
  gameArchive.sortPlayers(column);
};
</script>

<style lang="scss">
.top-list {
  overflow: auto;
}
</style>
