<template>
  <div>
    <div v-for="player in players" v-bind:key="player.id">
      <PlayerRow
        :player="player"
        :isCashgameRunning="isCashgameRunning"
        :isReportTimeEnabled="isCashgameRunning"
        @selected="onSelected"
        @deleteAction="onDeleteAction"
        @saveAction="onSaveAction"
        :canEdit="canEdit"
        :bunchId="bunchId"
      />
    </div>
    <div class="totals">
      <div class="title">Totals:</div>
      <div class="amounts">
        <div class="amount">
          <InlineIcon><BuyinIcon title="Total Buy in" /></InlineIcon> <CurrencyText :value="totalBuyin" />
        </div>
        <div class="amount">
          <InlineIcon><ReportIcon title="Total Stacks" /></InlineIcon> <CurrencyText :value="totalStacks" />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import PlayerRow from './PlayerRow.vue';
import CurrencyText from '@/components/Common/CurrencyText.vue';
import cashgameHelper from '@/CashgameHelper';
import { computed } from 'vue';
import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
import BuyinIcon from '../Icons/BuyinIcon.vue';
import ReportIcon from '../Icons/ReportIcon.vue';
import InlineIcon from '../Icons/InlineIcon.vue';

const props = defineProps<{
  bunchId: string;
  players: DetailedCashgamePlayer[];
  isCashgameRunning: boolean;
  canEdit: boolean;
}>();

const emit = defineEmits(['playerSelected', 'deleteAction', 'saveAction']);

const totalBuyin = computed(() => {
  return cashgameHelper.getTotalBuyin(props.players);
});

const totalStacks = computed(() => {
  return cashgameHelper.getTotalStacks(props.players);
});

const onSelected = (id: string) => {
  emit('playerSelected', id);
};

const onDeleteAction = (id: string) => {
  emit('deleteAction', id);
};

const onSaveAction = (data: any) => {
  emit('saveAction', data);
};
</script>
