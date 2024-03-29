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
        :localization="localization"
      />
    </div>
    <div class="totals">
      <div class="title">Totals:</div>
      <div class="amounts">
        <div class="amount">
          <InlineIcon><BuyinIcon title="Total Buy in" /></InlineIcon>
          <CurrencyText :value="totalBuyin" :localization="localization" />
        </div>
        <div class="amount">
          <InlineIcon><ReportIcon title="Total Stacks" /></InlineIcon>
          <CurrencyText :value="totalStacks" :localization="localization" />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import PlayerRow from './PlayerRow.vue';
import { CurrencyText } from '@/components/Common';
import cashgameHelper from '@/CashgameHelper';
import { computed } from 'vue';
import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
import { BuyinIcon, InlineIcon, ReportIcon } from '../Icons';
import { Localization } from '@/models/Localization';
import { SaveActionEmitData } from '@/models/SaveActionEmitData';

const props = defineProps<{
  bunchId: string;
  players: DetailedCashgamePlayer[];
  isCashgameRunning: boolean;
  canEdit: boolean;
  localization: Localization;
}>();

const emit = defineEmits<{
  playerSelected: [data: string];
  saveAction: [data: SaveActionEmitData];
  deleteAction: [data: string];
}>();

const totalBuyin = computed(() => cashgameHelper.getTotalBuyin(props.players));
const totalStacks = computed(() => cashgameHelper.getTotalStacks(props.players));
const onSelected = (id: string) => emit('playerSelected', id);
const onDeleteAction = (id: string) => emit('deleteAction', id);
const onSaveAction = (data: SaveActionEmitData) => emit('saveAction', data);
</script>
