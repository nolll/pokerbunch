<template>
  <div class="matrix" v-if="ready">
    <TableList>
      <thead>
        <tr>
          <TableListColumnHeader />
          <TableListColumnHeader>Player</TableListColumnHeader>
          <TableListColumnHeader>Total</TableListColumnHeader>
          <TableListColumnHeader>
            <CustomLink :url="url">Last game</CustomLink>
          </TableListColumnHeader>
        </tr>
      </thead>
      <tbody>
        <OverviewRow v-for="(player, index) in players" :player="player" :index="index" :key="player.id" :bunchId="slug" />
      </tbody>
    </TableList>
  </div>
</template>

<script setup lang="ts">
import urls from '@/urls';
import OverviewRow from '@/components/Overview/OverviewRow.vue';
import CustomLink from '@/components/Common/CustomLink.vue';
import TableList from '@/components/Common/TableList/TableList.vue';
import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';
import playerSorter from '@/PlayerSorter';
import archiveHelper from '@/ArchiveHelper';
import gameSorter from '@/GameSorter';
import { computed } from 'vue';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { CashgameSortOrder } from '@/models/CashgameSortOrder';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';

const props = defineProps<{
  games: ArchiveCashgame[];
  year: number;
  slug: string;
}>();

const games = computed((): ArchiveCashgame[] => {
  return gameSorter.sort(props.games, CashgameSortOrder.Date);
});

const players = computed((): CashgameListPlayerData[] => {
  return playerSorter.sort(archiveHelper.getPlayers(games.value), CashgamePlayerSortOrder.Winnings);
});

const url = computed(() => {
  return urls.cashgame.details(slug.value, lastGame.value.id);
});

const lastGame = computed(() => games.value[0]);
const slug = computed(() => props.slug);

const ready = computed(() => {
  return players.value.length > 0;
});
</script>
