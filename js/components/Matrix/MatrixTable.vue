<template>
  <div class="matrix" v-if="hasGames">
    <TableList>
      <thead>
        <tr>
          <TableListColumnHeader />
          <TableListColumnHeader>Player</TableListColumnHeader>
          <TableListColumnHeader>Winnings</TableListColumnHeader>
          <MatrixColumn v-for="game in games" :game="game" :slug="slug" :key="game.id" />
        </tr>
      </thead>
      <tbody>
        <MatrixRow
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
import MatrixColumn from './MatrixColumn.vue';
import MatrixRow from './MatrixRow.vue';
import { TableList, TableListColumnHeader } from '@/components/Common/TableList';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import archiveHelper from '@/ArchiveHelper';
import playerSorter from '@/PlayerSorter';
import { computed } from 'vue';
import { Localization } from '@/models/Localization';

const props = defineProps<{
  slug: string;
  games: ArchiveCashgame[];
  localization: Localization;
}>();

const hasGames = computed(() => props.games.length > 0);
const players = computed(() => playerSorter.sort(archiveHelper.getPlayers(props.games)));
</script>
