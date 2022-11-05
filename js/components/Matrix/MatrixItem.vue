<template>
  <TableListCell :is-numeric="true" :custom-classes="winnerClass">
    <template v-if="isInGame">
      <span class="matrix__value"><WinningsText :value="winnings" :showCurrency="false" /></span>
      <span class="matrix__transaction">in {{ buyin }}</span>
      <span class="matrix__transaction">out {{ stack }}</span>
    </template>
  </TableListCell>
</template>

<script setup lang="ts">
import { CashgamePlayerData } from '@/models/CashgamePlayerData';
import { CssClasses } from '@/models/CssClasses';
import TableListCell from '@/components/Common/TableList/TableListCell.vue';
import WinningsText from '@/components/Common/WinningsText.vue';
import { computed } from 'vue';

const props = defineProps<{
  game: CashgamePlayerData;
}>();

const buyin = computed(() => {
  return props.game.buyin;
});

const stack = computed(() => {
  return props.game.stack;
});

const winnings = computed(() => {
  return props.game.winnings;
});

const winnerClass = computed((): CssClasses => {
  return {
    matrix__winner: props.game.isWinner,
  };
});

const isInGame = computed(() => {
  return props.game.playedThisGame;
});
</script>
