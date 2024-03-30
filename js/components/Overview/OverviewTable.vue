<template>
  <div class="matrix">
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
        <OverviewRow
          v-for="(player, index) in players"
          :player="player"
          :index="index"
          :bunchId="slug"
          :localization="localization"
          :key="player.id"
        />
      </tbody>
    </TableList>
  </div>
</template>

<script setup lang="ts">
import urls from '@/urls';
import OverviewRow from '@/components/Overview/OverviewRow.vue';
import { CustomLink } from '@/components/Common';
import { TableList, TableListColumnHeader } from '@/components/Common/TableList';
import { computed } from 'vue';
import useParams from '@/composables/useParams';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import archiveHelper from '@/ArchiveHelper';
import playerSorter from '@/PlayerSorter';
import { BunchResponse } from '@/response/BunchResponse';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';
import { Localization } from '@/models/Localization';

const props = defineProps<{
  bunch: BunchResponse;
  games: ArchiveCashgame[];
  localization: Localization;
}>();

const { slug } = useParams();

const url = computed(() => {
  return urls.cashgame.details(slug.value, lastGame.value.id);
});

const lastGame = computed(() => {
  return props.games[0];
});

const players = computed((): CashgameListPlayerData[] => {
  return playerSorter.sort(archiveHelper.getPlayers(props.games), CashgamePlayerSortOrder.Winnings);
});
</script>
