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
        <TopListRow v-for="player in players" :player="player" :key="player.id" :bunchId="bunchId" :localization="localization" />
      </tbody>
    </TableList>
  </div>
</template>

<script setup lang="ts">
import TopListRow from './TopListRow.vue';
import { TableList, TableListColumnHeader } from '@/components/Common/TableList';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import playerSorter from '@/PlayerSorter';
import archiveHelper from '@/ArchiveHelper';
import { computed, ref } from 'vue';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';
import { Localization } from '@/models/Localization';

const props = defineProps<{
  bunchId: string;
  games: ArchiveCashgame[];
  localization: Localization;
}>();

const players = computed(() => playerSorter.sort(archiveHelper.getPlayers(props.games), orderedBy.value));

const orderedBy = ref(CashgamePlayerSortOrder.Winnings);

const sort = (column: CashgamePlayerSortOrder) => {
  orderedBy.value = column;
};
</script>

<style lang="scss">
.top-list {
  overflow: auto;
}
</style>
