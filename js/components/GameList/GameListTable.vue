<template>
  <div class="game-list">
    <TableList>
      <thead>
        <tr>
          <TableListColumnHeader :ordered-by="sortOrder" v-on:sort="sort" sort-name="date">Date</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="sortOrder" v-on:sort="sort" sort-name="playercount">Players</TableListColumnHeader>
          <TableListColumnHeader>Location</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="sortOrder" v-on:sort="sort" sort-name="duration">Duration</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="sortOrder" v-on:sort="sort" sort-name="turnover">Turnover</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="sortOrder" v-on:sort="sort" sort-name="averagebuyin"> Average Buyin </TableListColumnHeader>
        </tr>
      </thead>
      <tbody class="list">
        <GameListRow :slug="slug" v-for="game in sortedGames" :game="game" :key="game.id" />
      </tbody>
    </TableList>
  </div>
</template>

<script setup lang="ts">
import GameListRow from './GameListRow.vue';
import TableList from '@/components/Common/TableList/TableList.vue';
import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';
import { computed, ref } from 'vue';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import gameSorter from '@/GameSorter';
import { CashgameSortOrder } from '@/models/CashgameSortOrder';

const props = defineProps<{
  slug: string;
  games: ArchiveCashgame[];
}>();

const sortOrder = ref(CashgameSortOrder.Date);
const sortedGames = computed((): ArchiveCashgame[] => {
  return gameSorter.sort(props.games, sortOrder.value);
});

const sort = (column: CashgameSortOrder) => {
  sortOrder.value = column;
};
</script>

<style lang="scss">
.game-list {
  overflow: auto;
}
</style>
