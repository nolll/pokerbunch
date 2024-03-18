<template>
  <div class="player-row">
    <div class="player-row__row-wrapper">
      <div class="player-row__name-and-time">
        <div>
          <div class="player-color-box" :style="{ backgroundColor: player.color }" @click="onSelected"></div>
          <a class="player-row__name" href="#" @click="toggle">{{ player.name }}</a>
          <InlineIcon><CashedOutIcon title="Cashed out" v-if="showCheckmark" /></InlineIcon>
        </div>
        <div class="time" v-if="isReportTimeEnabled">
          <InlineIcon><TimeIcon title="Last report" /></InlineIcon> <span>{{ lastReportTime }}</span>
        </div>
      </div>
      <div class="player-row__amounts">
        <div>
          <i title="Buy in" class="icon-signin"></i> <CurrencyText :value="calculatedBuyin" :localization="localization" />
        </div>
        <div><i title="Stack" class="icon-reorder"></i> <CurrencyText :value="stack" :localization="localization" /></div>
        <div><WinningsText :value="winnings" :localization="localization" /></div>
      </div>
    </div>
    <div v-if="showDetails">
      <div class="player-row__chart">
        <CashgameActionChart :player="player" />
      </div>
      <div class="player-row__link">
        <CustomLink :url="url">View player</CustomLink>
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
import CustomLink from '@/components/Common/CustomLink.vue';
import PlayerAction from './PlayerAction.vue';
import urls from '@/urls';
import CashgameActionChart from '@/components/CashgameActionChart.vue';
import CurrencyText from '@/components/Common/CurrencyText.vue';
import WinningsText from '@/components/Common/WinningsText.vue';
import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
import { computed, ref } from 'vue';
import CashedOutIcon from '../Icons/CashedOutIcon.vue';
import TimeIcon from '../Icons/TimeIcon.vue';
import InlineIcon from '../Icons/InlineIcon.vue';
import { Localization } from '@/models/Localization';

const props = defineProps<{
  bunchId: string;
  player: DetailedCashgamePlayer;
  isCashgameRunning: boolean;
  canEdit: boolean;
  localization: Localization;
}>();

const emit = defineEmits<{
  selected: [data: string];
  saveAction: [data: SaveActionEmitData];
  deleteAction: [data: string];
}>();

const isExpanded = ref(false);

const hasCashedOut = computed(() => {
  return props.player.hasCashedOut();
});

const showCheckmark = computed(() => {
  return props.isCashgameRunning && hasCashedOut.value;
});

const isReportTimeEnabled = computed(() => {
  return props.isCashgameRunning;
});

const lastReportTime = computed(() => {
  return props.player.getLastReportTime();
});

const calculatedBuyin = computed(() => {
  return props.player.getBuyin();
});

const stack = computed(() => {
  return props.player.getStack();
});

const winnings = computed(() => {
  return props.player.getWinnings();
});

const url = computed(() => {
  return urls.player.details(props.bunchId, props.player.id);
});

const showDetails = computed(() => {
  return isExpanded.value;
});

const expand = () => {
  isExpanded.value = true;
};

const collapse = () => {
  isExpanded.value = false;
};

const toggle = () => {
  isExpanded.value = !isExpanded.value;
};

const onSelected = () => {
  emit('selected', props.player.id);
};

const onDeleteAction = (id: string) => {
  emit('deleteAction', id);
};

const onSaveAction = (data: SaveActionEmitData) => {
  emit('saveAction', data);
};

const click = () => {
  toggle();
};
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
  flex: 11;
}

.player-row__chart,
.player-row__actions {
  padding: 8px;
}
</style>
