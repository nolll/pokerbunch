<template>
  <TableListCell :is-numeric="true">
    <span class="matrix__value" v-if="playedThisYear"><WinningsText :value="winnings" :showCurrency="false" /></span>
  </TableListCell>
</template>

<script setup lang="ts">
import { CashgamePlayerYearlyResult } from '@/models/CashgamePlayerYearlyResult';
import TableListCell from '@/components/Common/TableList/TableListCell.vue';
import WinningsText from '@/components/Common/WinningsText.vue';
import { computed } from 'vue';

const props = defineProps<{
  year: CashgamePlayerYearlyResult;
}>();

const winnings = computed(() => {
  return props.year.winnings;
});

const playedThisYear = computed(() => {
  return props.year.playedThisYear;
});
</script>
