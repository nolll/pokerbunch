<template>
  <div class="top-list">
    <TableList>
      <thead>
        <tr>
          <TableListColumnHeader />
          <TableListColumnHeader>Player</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="playerSortOrder" v-on:sort="sort" sort-name="winnings">Winnings</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="playerSortOrder" v-on:sort="sort" sort-name="buyin">Buyin</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="playerSortOrder" v-on:sort="sort" sort-name="stack">Cashout</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="playerSortOrder" v-on:sort="sort" sort-name="time">Time</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="playerSortOrder" v-on:sort="sort" sort-name="gamecount">Games</TableListColumnHeader>
          <TableListColumnHeader :ordered-by="playerSortOrder" v-on:sort="sort" sort-name="winrate">Win rate</TableListColumnHeader>
        </tr>
      </thead>
      <tbody class="list">
        <TopListRow v-for="player in players" :player="player" :key="player.id" :bunchId="slug" />
      </tbody>
    </TableList>
  </div>
</template>

<script setup lang="ts">
import TopListRow from './TopListRow.vue';
import TableList from '@/components/Common/TableList/TableList.vue';
import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';
import { computed, ref } from 'vue';
import archiveHelper from '@/ArchiveHelper';
import playerSorter from '@/PlayerSorter';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';

const props = defineProps<{
  slug: string;
  games: ArchiveCashgame[];
}>();

const playerSortOrder = ref(CashgamePlayerSortOrder.Winnings);

const players = computed((): CashgameListPlayerData[] => {
  return playerSorter.sort(archiveHelper.getPlayers(props.games), playerSortOrder.value);
});

const sort = (column: CashgamePlayerSortOrder) => {
  playerSortOrder.value = column;
};
</script>

<style lang="scss">
.top-list {
  overflow: auto;
}
</style>
