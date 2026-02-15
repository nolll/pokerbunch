<template>
  <div>
    <div v-for="(player, index) in players" v-bind:key="player.id">
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
        :color="getColor(index)"
      />
    </div>
    <div class="totals">
      <div class="title">Totals:</div>
      <div class="amounts">
        <div class="amount">
          <BuyinIcon title="Total Buyin" /> <CurrencyText :value="totalBuyin" :localization="localization" />
        </div>
        <div class="amount">
          <ReportIcon title="Total Stacks" /> <CurrencyText :value="totalStacks" :localization="localization" />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import PlayerRow from './PlayerRow.vue';
import { CurrencyText } from '@/components/Common';
import { computed } from 'vue';
import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
import { BuyinIcon, ReportIcon } from '../Icons';
import { Localization } from '@/models/Localization';
import { SaveActionEmitData } from '@/models/SaveActionEmitData';
import { getColor } from '@/colors';

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

const totalBuyin = computed(() => props.players.reduce((sum, player) => sum + player.getBuyin(), 0));
const totalStacks = computed(() => props.players.reduce((sum, player) => sum + player.getStack(), 0));
const onSelected = (id: string) => emit('playerSelected', id);
const onDeleteAction = (id: string) => emit('deleteAction', id);
const onSaveAction = (data: SaveActionEmitData) => emit('saveAction', data);
</script>
