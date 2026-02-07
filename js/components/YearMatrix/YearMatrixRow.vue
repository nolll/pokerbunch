<template>
  <TableListRow>
    <TableListCell :is-numeric="true">{{ player.rank }}.</TableListCell>
    <TableListCell>
      <CustomLink :url="url">{{ name }}</CustomLink>
    </TableListCell>
    <TableListCell :is-numeric="true"
      ><WinningsText :value="winnings" :show-currency="true" :localization="localization"
    /></TableListCell>
    <YearMatrixItem v-for="year in player.years" :year="year" :key="year.year" />
  </TableListRow>
</template>

<script setup lang="ts">
import YearMatrixItem from './YearMatrixItem.vue';
import { CustomLink, WinningsText } from '@/components/Common';
import urls from '@/urls';
import { CashgamePlayerYearlyResultCollection } from '@/models/CashgamePlayerYearlyResultCollection';
import { TableListCell, TableListRow } from '@/components/Common/TableList';
import { computed } from 'vue';
import { Localization } from '@/models/Localization';

const props = defineProps<{
  bunchId: string;
  player: CashgamePlayerYearlyResultCollection;
  localization: Localization;
}>();

const url = computed(() => {
  return urls.player.details(props.bunchId, props.player.id);
});

const name = computed(() => {
  return props.player.name;
});

const winnings = computed(() => {
  return props.player.winnings;
});
</script>
