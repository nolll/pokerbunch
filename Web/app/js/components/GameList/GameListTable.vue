<template>
  <div class="game-list">
    <TableList>
      <thead>
        <tr>
          <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="date">Date</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="playercount">Players</TableListColumnHeader>
          <TableListColumnHeader>Location</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="duration">Duration</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="turnover">Turnover</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="orderedBy" v-on:sort="sort" sort-name="averagebuyin">
            Average Buyin
          </TableListColumnHeader>
        </tr>
      </thead>
      <tbody class="list">
        <GameListRow v-for="game in sortedGames" :game="game" :key="game.id" />
      </tbody>
    </TableList>
  </div>
</template>

<script setup lang="ts">
import GameListRow from './GameListRow.vue';
import TableList from '@/components/Common/TableList/TableList.vue';
import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';
import { computed } from 'vue';
import useBunches from '@/composables/useBunches';
import useGameArchive from '@/composables/useGameArchive';

const bunches = useBunches();
const gameArchive = useGameArchive();

const ready = computed(() => {
  return bunches.bunchReady.value && gameArchive.sortedGames.value.length > 0;
});

const sortedGames = computed(() => {
  return gameArchive.sortedGames.value;
});

const orderedBy = computed(() => {
  return gameArchive.gameSortOrder.value;
});

const sort = (column: string) => {
  gameArchive.sortGames(column);
};
</script>

<style lang="scss" scoped>
.game-list {
  overflow: auto;
}
</style>
