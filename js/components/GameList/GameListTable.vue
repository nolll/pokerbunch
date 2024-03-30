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
        <GameListRow v-for="game in sortedGames" :game="game" :bunch="bunch" :localization="localization" :key="game.id" />
      </tbody>
    </TableList>
  </div>
</template>

<script setup lang="ts">
import GameListRow from './GameListRow.vue';
import { TableList, TableListColumnHeader } from '@/components/Common/TableList';
import { computed, ref } from 'vue';
import { BunchResponse } from '@/response/BunchResponse';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import gameSorter from '@/GameSorter';
import { CashgameSortOrder } from '@/models/CashgameSortOrder';
import { Localization } from '@/models/Localization';

const props = defineProps<{
  bunch: BunchResponse;
  games: ArchiveCashgame[];
  localization: Localization;
}>();

const orderedBy = ref(CashgameSortOrder.Date);

const sortedGames = computed(() => {
  return gameSorter.sort(props.games, orderedBy.value);
});

const sort = (column: CashgameSortOrder) => {
  orderedBy.value = column;
};
</script>

<style lang="scss">
.game-list {
  overflow: auto;
}
</style>
