<template>
  <TableListRow>
    <TableListCell :is-numeric="true">{{ player.rank }}.</TableListCell>
    <TableListCell
      ><CustomLink :url="url">{{ player.name }}</CustomLink></TableListCell
    >
    <TableListCell :is-numeric="true"
      ><WinningsText :value="winnings" :show-currency="true" :localization="localization"
    /></TableListCell>
    <TableListCell :is-numeric="true"><CurrencyText :value="buyin" :localization="localization" /></TableListCell>
    <TableListCell :is-numeric="true"><CurrencyText :value="cashout" :localization="localization" /></TableListCell>
    <TableListCell><DurationText :value="time" /></TableListCell>
    <TableListCell :is-numeric="true">{{ player.gameCount }}</TableListCell>
    <TableListCell :is-numeric="true"><WinrateText :value="winrate" :localization="localization" /></TableListCell>
  </TableListRow>
</template>

<script setup lang="ts">
import { CustomLink } from '@/components/Common';
import urls from '@/urls';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import TableListRow from '@/components/Common/TableList/TableListRow.vue';
import TableListCell from '@/components/Common/TableList/TableListCell.vue';
import { CurrencyText, DurationText, WinningsText, WinrateText } from '@/components/Common';
import { computed } from 'vue';
import { Localization } from '@/models/Localization';

const props = defineProps<{
  bunchId: string;
  player: CashgameListPlayerData;
  localization: Localization;
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
