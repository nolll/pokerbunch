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
import { filterGames } from '@/helpers/gameArchiveHelpers';
import { CashgameSortOrder } from '@/models/CashgameSortOrder';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';

const props = defineProps<{
  games: ArchiveCashgame[];
  year: number;
  slug: string;
}>();

const players = computed(() => {
  return currentYearPlayers.value;
});

const currentYearGames = computed((): ArchiveCashgame[] => {
  const selectedGames = filterGames(props.games, props.year);
  return gameSorter.sort(selectedGames, CashgameSortOrder.Date);
});

const currentYearPlayers = computed((): CashgameListPlayerData[] => {
  return playerSorter.sort(archiveHelper.getPlayers(currentYearGames.value), CashgamePlayerSortOrder.Winnings);
});

const url = computed(() => {
  return urls.cashgame.details(slug.value, lastGame.value.id);
});

const lastGame = computed(() => {
  return currentYearGames.value[0];
});

const slug = computed(() => {
  return props.slug;
});

const ready = computed(() => {
  return currentYearPlayers.value.length > 0;
});
</script>
