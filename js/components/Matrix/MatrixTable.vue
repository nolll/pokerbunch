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
        <MatrixRow v-for="(player, index) in players" :player="player" :index="index" :key="player.id" :bunchId="slug" />
      </tbody>
    </TableList>
  </div>
</template>

<script setup lang="ts">
import MatrixColumn from './MatrixColumn.vue';
import MatrixRow from './MatrixRow.vue';
import TableList from '@/components/Common/TableList/TableList.vue';
import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import archiveHelper from '@/ArchiveHelper';
import playerSorter from '@/PlayerSorter';
import { computed } from 'vue';

const props = defineProps<{
  slug: string;
  games: ArchiveCashgame[];
}>();

const hasGames = computed(() => props.games.length > 0);
const players = computed(() => playerSorter.sort(archiveHelper.getPlayers(props.games)));
</script>
