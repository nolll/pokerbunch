<template>
  <div class="player-row">
    <div class="row-wrapper" @click="toggle">
      <div class="name-and-time">
        <div class="name">
          <a @click.stop="" :href="url">{{ player.name }}</a>
          <Icon name="checkmark" title="Cashed out" v-if="showCheckmark" />
        </div>
        <div class="time" v-if="isReportTimeEnabled && showDetails">
          <Icon name="time" title="Last report" /> <span>{{ lastReportTime }}</span>
        </div>
      </div>
      <div class="row-inner">
        <div class="small-chart" v-if="!showDetails">
          <CashgameActionChartSmall :player="player" />
        </div>
        <div class="amounts">
          <div class="amount-item">
            <Icon name="buyin" title="Buyin" />
            <CurrencyText :value="calculatedBuyin" :localization="localization" />
          </div>
          <div class="amount-item">
            <Icon name="report" title="Stack" />
            <CurrencyText :value="stack" :localization="localization" />
          </div>
          <div class="amount-item">
            <WinningsText :value="winnings" :localization="localization" :showCurrency="true" />
          </div>
        </div>
      </div>
    </div>
    <div v-if="showDetails">
      <div class="chart">
        <CashgameActionChart :player="player" />
      </div>
      <div class="actions">
        <TableList :is-wide="true">
          <thead>
            <tr>
              <TableListColumnHeader>Type</TableListColumnHeader>
              <TableListColumnHeader>Time</TableListColumnHeader>
              <TableListColumnHeader>Stack</TableListColumnHeader>
              <TableListColumnHeader>Added</TableListColumnHeader>
              <TableListColumnHeader></TableListColumnHeader>
            </tr>
          </thead>
          <tbody class="list">
            <TableListRow v-for="action in player.actions">
              <TableListCell>
                <Icon :name="action.type" :title="getTypeName(action)" />
              </TableListCell>
              <TableListCell>
                <template v-if="isEditing(action)">
                  <input v-model="editTime" type="time" />
                </template>
                <template v-else>
                  {{ getTime(action) }}
                </template>
              </TableListCell>
              <TableListCell>
                <template v-if="isEditing(action)">
                  <input class="numberfield" v-model="editStack" type="text" inputmode="numeric" pattern="[0-9]*" />
                </template>
                <template v-else>
                  {{ getFormattedAmount(action.stack) }}
                </template>
              </TableListCell>
              <TableListCell>
                <template v-if="action.type === DetailedCashgameResponseActionType.Buyin">
                  <template v-if="isEditing(action)">
                    <input class="numberfield" v-model="editAdded" type="text" inputmode="numeric" pattern="[0-9]*" />
                  </template>
                  <template v-else>
                    {{ getFormattedAmount(action.added!) }}
                  </template>
                </template>
              </TableListCell>
              <TableListCell>
                <div class="actions-column">
                  <template v-if="isEditing(action)">
                    <IconButton>
                      <Icon name="checkmark" v-on:click="() => save(action)" title="Save" />
                    </IconButton>
                    <IconButton>
                      <Icon name="close" v-on:click="() => cancelEdit()" title="Cancel" />
                    </IconButton>
                  </template>
                  <template v-else>
                    <IconButton>
                      <Icon name="delete" v-on:click="() => deleteRow(action)" title="Delete" />
                    </IconButton>
                    <IconButton>
                      <Icon name="edit" v-on:click="() => editRow(action)" title="Edit" />
                    </IconButton>
                  </template>
                </div>
              </TableListCell>
            </TableListRow>
          </tbody>
        </TableList>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import urls from '@/urls';
import CashgameActionChart from '@/components/CurrentGame/CashgameActionChart.vue';
import CashgameActionChartSmall from '@/components/CurrentGame/CashgameActionChartSmall.vue';
import { CurrencyText, WinningsText } from '@/components/Common';
import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
import { computed, ref } from 'vue';
import { Icon } from '../Icons';
import { Localization } from '@/models/Localization';
import { SaveActionEmitData } from '@/models/SaveActionEmitData';
import { TableList, TableListColumnHeader, TableListRow, TableListCell } from '@/components/Common/TableList';
import { DetailedCashgameAction } from '@/models/DetailedCashgameAction';
import format from '@/format';
import { DetailedCashgameResponseActionType } from '@/response/DetailedCashgameResponseActionType';
import { IconButton } from '@/components/Common';
import dayjs from 'dayjs';

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

const editingRow = ref<DetailedCashgameAction | null>(null);
const editStack = ref<number>(0);
const editAdded = ref<number>(0);
const editTime = ref<string>(format.forTimeInput(new Date()));
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
const deleteRow = (action: DetailedCashgameAction) => emit('deleteAction', action.id);
const getTime = (action: DetailedCashgameAction) => format.hourMinute(action.time);
const getFormattedAmount = (amount: number) => format.currency(amount, props.localization);

const getTypeName = (action: DetailedCashgameAction): string => {
  if (action.type === DetailedCashgameResponseActionType.Buyin) return 'Buyin';
  if (action.type === DetailedCashgameResponseActionType.Cashout) return 'Cashout';
  return 'Report';
};

const isEditing = (action: DetailedCashgameAction) => editingRow.value && editingRow.value.id == action.id;

const editRow = (action: DetailedCashgameAction) => {
  editStack.value = action.stack;
  editAdded.value = action.added ?? 0;
  editTime.value = format.forTimeInput(action.time);
  editingRow.value = action;
};

const getNewTime = (date: Date, time: string) => {
  var d = dayjs(date);
  var parts = time.split(':');
  var h = parseInt(parts[0]);
  var m = parseInt(parts[1]);
  d = d.hour(h);
  d = d.minute(m);
  return d.toDate();
};

const cancelEdit = () => {
  editingRow.value = null;
};

const save = (action: DetailedCashgameAction) => {
  const data: SaveActionEmitData = {
    id: action.id,
    time: getNewTime(action.time, editTime.value),
    stack: editStack.value,
    added: action.type == DetailedCashgameResponseActionType.Buyin ? editAdded.value : null,
  };
  //console.log('save', data);
  emit('saveAction', data);
  editingRow.value = null;
};
</script>

<style lang="scss" scoped>
.player-row {
  padding: 5px 0;
  border-bottom: 1px solid #eee;
}

.row-wrapper {
  position: relative;
}

.row-inner {
  display: flex;
  justify-content: flex-end;
}

.name-and-time {
  position: absolute;
  background: #fff;
  z-index: 1;
  padding-right: 5px;
}

.name {
  display: flex;
  align-items: center;
  gap: 5px;
}

.amounts {
  width: 25%;
}

.amount-item {
  display: flex;
  align-items: center;
  justify-content: flex-end;
  flex-wrap: nowrap;
  gap: 3px;
  margin-right: 5px;
}

.chart,
.actions {
  padding: 8px;
}

.small-chart {
  width: 75%;
  max-width: 300px;
}

.actions-column {
  display: flex;
  gap: 0.5rem;
  justify-content: right;
}

.time {
  display: flex;
  align-items: center;
  gap: 3px;
}
</style>
