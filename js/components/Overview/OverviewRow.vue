<template>
  <TableListRow>
    <TableListCell :is-numeric="true">{{ rank }}.</TableListCell>
    <TableListCell>
      <CustomLink :url="url">{{ name }}</CustomLink>
    </TableListCell>
    <TableListCell :is-numeric="true"
      ><WinningsText :value="winnings" :show-currency="true" :localization="localization"
    /></TableListCell>
    <OverviewItem :game="game" />
  </TableListRow>
</template>

<script setup lang="ts">
import OverviewItem from './OverviewItem.vue';
import { CustomLink } from '@/components/Common';
import urls from '@/urls';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { TableListCell, TableListRow } from '@/components/Common/TableList';
import { WinningsText } from '@/components/Common';
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
const game = computed(() => props.player.gameResults[0]);
</script>
