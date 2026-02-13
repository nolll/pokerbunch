<template>
  <TableListCell :is-numeric="true">
    <span class="matrix__value" v-if="playedThisYear"><WinningsText :value="winnings" :showCurrency="false" /></span>
  </TableListCell>
</template>

<script setup lang="ts">
import { CashgamePlayerYearlyResult } from '@/models/CashgamePlayerYearlyResult';
import { TableListCell } from '@/components/Common/TableList';
import { WinningsText } from '@/components/Common';
import { computed } from 'vue';

const props = defineProps<{
  year: CashgamePlayerYearlyResult;
}>();

const winnings = computed(() => props.year.winnings);
const playedThisYear = computed(() => props.year.playedThisYear);
</script>
