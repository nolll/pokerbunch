<template>
  <TableListRow>
    <TableListCell :is-numeric="true">{{ rank }}.</TableListCell>
    <TableListCell>
      <CustomLink :url="url">{{ name }}</CustomLink>
    </TableListCell>
    <TableListCell :is-numeric="true"
      ><WinningsText :value="winnings" :show-currency="true" :localization="localization"
    /></TableListCell>
    <MatrixItem v-for="game in player.gameResults" :game="game" :localization="localization" :key="game.gameId" />
  </TableListRow>
</template>

<script setup lang="ts">
import urls from '@/urls';
import MatrixItem from './MatrixItem.vue';
import CustomLink from '@/components/Common/CustomLink.vue';
import TableListRow from '@/components/Common/TableList/TableListRow.vue';
import TableListCell from '@/components/Common/TableList/TableListCell.vue';
import WinningsText from '@/components/Common/WinningsText.vue';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { computed } from 'vue';
import { Localization } from '@/models/Localization';

const props = defineProps<{
  bunchId: string;
  player: CashgameListPlayerData;
  index: number;
  localization: Localization;
}>();

const url = computed(() => urls.player.details(props.bunchId, props.player.id));
const name = computed(() => props.player.name);
const rank = computed(() => props.index + 1);
const winnings = computed(() => props.player.winnings);
</script>
