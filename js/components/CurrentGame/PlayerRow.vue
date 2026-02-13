<template>
  <div class="player-row">
    <div class="player-row__row-wrapper" @click="toggle">
      <div class="player-row__name-and-time">
        <div>
          <!-- <div class="player-color-box" :style="{ backgroundColor: color }" @click="onSelected"></div> -->
          <a class="player-row__name" @click.stop="" :href="url">{{ player.name }}</a>
          <InlineIcon><CashedOutIcon title="Cashed out" v-if="showCheckmark" /></InlineIcon>
        </div>
        <div class="time" v-if="isReportTimeEnabled">
          <InlineIcon><TimeIcon title="Last report" /></InlineIcon> <span>{{ lastReportTime }}</span>
        </div>
      </div>
      <div class="player-row__amounts">
        <div>
          <InlineIcon><BuyinIcon title="Buy in" /></InlineIcon>
          <CurrencyText :value="calculatedBuyin" :localization="localization" />
        </div>
        <div>
          <InlineIcon><ReportIcon title="Total Stacks" /></InlineIcon>
          <CurrencyText :value="stack" :localization="localization" />
        </div>
        <div><WinningsText :value="winnings" :localization="localization" :showCurrency="true" /></div>
      </div>
      <div class="player-row__small-chart">
        <CashgameActionChartSmall :player="player" />
      </div>
    </div>
    <div v-if="showDetails">
      <div class="player-row__chart">
        <CashgameActionChart :player="player" />
      </div>
      <div class="player-row__actions">
        <PlayerAction
          v-for="action in player.actions"
          :action="action"
          :key="action.id"
          :localization="localization"
          @deleteAction="onDeleteAction"
          @saveAction="onSaveAction"
          :canEdit="canEdit"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import PlayerAction from './PlayerAction.vue';
import urls from '@/urls';
import CashgameActionChart from '@/components/CurrentGame/CashgameActionChart.vue';
import CashgameActionChartSmall from '@/components/CurrentGame/CashgameActionChartSmall.vue';
import { CurrencyText, WinningsText } from '@/components/Common';
import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
import { computed, ref } from 'vue';
import { BuyinIcon, CashedOutIcon, InlineIcon, ReportIcon, TimeIcon } from '../Icons';
import { Localization } from '@/models/Localization';
import { SaveActionEmitData } from '@/models/SaveActionEmitData';

const props = defineProps<{
  bunchId: string;
  player: DetailedCashgamePlayer;
  isCashgameRunning: boolean;
  canEdit: boolean;
  localization: Localization;
  color: string;
}>();

const emit = defineEmits<{
  selected: [data: string];
  saveAction: [data: SaveActionEmitData];
  deleteAction: [data: string];
}>();

const isExpanded = ref(false);
const hasCashedOut = computed(() => props.player.hasCashedOut());
const showCheckmark = computed(() => props.isCashgameRunning && hasCashedOut.value);
const isReportTimeEnabled = computed(() => props.isCashgameRunning);
const lastReportTime = computed(() => props.player.getLastReportTime());
const calculatedBuyin = computed(() => props.player.getBuyin());
const stack = computed(() => props.player.getStack());
const winnings = computed(() => props.player.getWinnings());
const url = computed(() => urls.player.details(props.bunchId, props.player.id));
const showDetails = computed(() => isExpanded.value);
const toggle = () => (isExpanded.value = !isExpanded.value);
const onDeleteAction = (id: string) => emit('deleteAction', id);
const onSaveAction = (data: SaveActionEmitData) => emit('saveAction', data);
</script>

<style lang="scss">
.player-row {
  padding: 5px 0;
  border-bottom: 1px solid #eee;
}

.player-row__row-wrapper {
  display: flex;
}

.player-row__link {
  padding: 8px;
}

.player-row__name-and-time {
  flex: 9;
}

.player-row__name {
  margin-right: 0.5rem;
}

.player-row__amounts {
  flex: 7;
}

.player-row__chart,
.player-row__actions {
  padding: 8px;
}

.player-row__small-chart {
  flex: 4;
}
</style>
