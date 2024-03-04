<template>
  <TableListRow>
    <TableListCell :is-numeric="true">{{ rank }}.</TableListCell>
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
import CustomLink from '@/components/Common/CustomLink.vue';
import urls from '@/urls';
import { CashgamePlayerYearlyResultCollection } from '@/models/CashgamePlayerYearlyResultCollection';
import TableListRow from '@/components/Common/TableList/TableListRow.vue';
import TableListCell from '@/components/Common/TableList/TableListCell.vue';
import WinningsText from '@/components/Common/WinningsText.vue';
import { computed } from 'vue';
import { Localization } from '@/models/Localization';

const props = defineProps<{
  bunchId: string;
  player: CashgamePlayerYearlyResultCollection;
  index: number;
  localization: Localization;
}>();

const url = computed(() => {
  return urls.player.details(props.bunchId, props.player.id);
});

const name = computed(() => {
  return props.player.name;
});

const rank = computed(() => {
  return props.index + 1;
});

const winnings = computed(() => {
  return props.player.winnings;
});
</script>
