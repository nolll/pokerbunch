<template>
  <TableListRow>
    <TableListCell :is-numeric="true">{{ player.rank }}.</TableListCell>
    <TableListCell
      ><CustomLink :url="url">{{ player.name }}</CustomLink></TableListCell
    >
    <TableListCell :is-numeric="true"><WinningsText :value="winnings" /></TableListCell>
    <TableListCell :is-numeric="true"><CurrencyText :value="buyin" /></TableListCell>
    <TableListCell :is-numeric="true"><CurrencyText :value="cashout" /></TableListCell>
    <TableListCell><DurationText :value="time" /></TableListCell>
    <TableListCell :is-numeric="true">{{ player.gameCount }}</TableListCell>
    <TableListCell :is-numeric="true"><WinrateText :value="winrate" /></TableListCell>
  </TableListRow>
</template>

<script setup lang="ts">
import CustomLink from '@/components/Common/CustomLink.vue';
import urls from '@/urls';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import TableListRow from '@/components/Common/TableList/TableListRow.vue';
import TableListCell from '@/components/Common/TableList/TableListCell.vue';
import WinningsText from '@/components/Common/WinningsText.vue';
import WinrateText from '@/components/Common/WinrateText.vue';
import CurrencyText from '@/components/Common/CurrencyText.vue';
import DurationText from '@/components/Common/DurationText.vue';
import { computed } from 'vue';

const props = defineProps<{
  bunchId: string;
  player: CashgameListPlayerData;
}>();

const url = computed(() => {
  return urls.player.details(props.bunchId, props.player.id);
});

const winnings = computed(() => {
  return props.player.winnings;
});

const buyin = computed(() => {
  return props.player.buyin;
});

const cashout = computed(() => {
  return props.player.stack;
});

const winrate = computed(() => {
  return props.player.winrate;
});

const time = computed(() => {
  return props.player.playedTimeInMinutes;
});
</script>
