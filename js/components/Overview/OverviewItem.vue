<template>
  <TableListCell :is-numeric="true">
    <span class="matrix__value" v-if="isInGame"><WinningsText :value="winnings" /></span>
  </TableListCell>
</template>

<script setup lang="ts">
import { CashgamePlayerData } from '@/models/CashgamePlayerData';
import { TableListCell } from '@/components/Common/TableList';
import { WinningsText } from '@/components/Common';
import { computed } from 'vue';

const props = defineProps<{
  game: CashgamePlayerData;
}>();

const winnings = computed(() => {
  return props.game.winnings;
});

const isInGame = computed(() => {
  return props.game.playedThisGame;
});
</script>
