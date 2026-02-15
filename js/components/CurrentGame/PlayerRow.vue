<template>
  <div class="player-row">
    <div class="player-row__row-wrapper" @click="toggle">
      <div class="player-row__name-and-time">
        <div>
          <!-- <div class="player-color-box" :style="{ backgroundColor: color }" @click="onSelected"></div> -->
          <a class="player-row__name" @click.stop="" :href="url">{{ player.name }}</a>
          <CashedOutIcon title="Cashed out" v-if="showCheckmark"></CashedOutIcon>
        </div>
        <div class="time" v-if="isReportTimeEnabled">
          <TimeIcon title="Last report" /> <span>{{ lastReportTime }}</span>
        </div>
      </div>
      <div class="player-row__amounts">
        <div><BuyinIcon title="Buyin" /> <CurrencyText :value="calculatedBuyin" :localization="localization" /></div>
        <div><ReportIcon title="Stack" /> <CurrencyText :value="stack" :localization="localization" /></div>
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
        <DataTable
          size="small"
          v-model:editingRows="editingRows"
          :value="player.actions"
          dataKey="id"
          editMode="row"
          @row-edit-init="onRowEditInit"
          @row-edit-save="onRowEditcomplete"
          @row-edit-cancel="onRowEditCancel"
        >
          <Column header="Type">
            <template #body="{ data }">
              <i :class="getTypeIcon(data)" :title="getTypeName(data)"></i>
            </template>
          </Column>
          <Column header="Time">
            <template #body="{ data }">
              {{ getTime(data) }}
            </template>
            <template #editor="{ data }">
              <DatePicker v-model="data.time" timeOnly fluid />
            </template>
          </Column>
          <Column header="Stack">
            <template #body="{ data }">
              {{ getFormattedAmount(data.stack) }}
            </template>
            <template #editor="{ data }">
              <InputNumber v-model="data.stack" fluid />
            </template>
          </Column>
          <Column header="Added">
            <template #body="{ data }">
              <template v-if="data.type === DetailedCashgameResponseActionType.Buyin">
                {{ getFormattedAmount(data.added) }}
              </template>
            </template>
            <template #editor="{ data }">
              <InputNumber v-if="data.type === DetailedCashgameResponseActionType.Buyin" v-model="data.added" fluid />
            </template>
          </Column>
          <Column v-if="canEdit" style="width: 10%; max-width: 8rem">
            <template #body="{ data }">
              <div class="actions-column">
                <Button
                  v-on:click="() => onDeleteAction(data.id)"
                  variant="text"
                  severity="danger"
                  tooltip="Delete"
                  icon="pi pi-trash"
                ></Button>
              </div>
            </template>
            <template #editor="{ data }"></template>
          </Column>
          <Column v-if="canEdit" :rowEditor="true" style="width: 10%; max-width: 8rem" bodyStyle="text-align:center"></Column>
        </DataTable>
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
import { BuyinIcon, CashedOutIcon, ReportIcon, TimeIcon } from '../Icons';
import { Localization } from '@/models/Localization';
import { SaveActionEmitData } from '@/models/SaveActionEmitData';
import {
  Button,
  DatePicker,
  InputNumber,
  Column,
  DataTable,
  DataTableRowEditInitEvent,
  DataTableRowEditSaveEvent,
  DataTableRowEditCancelEvent,
} from 'primevue';
import { DetailedCashgameAction } from '@/models/DetailedCashgameAction';
import format from '@/format';
import { DetailedCashgameResponseActionType } from '@/response/DetailedCashgameResponseActionType';

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

const editingRows = ref<DetailedCashgameAction[]>([]);
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
const getTime = (action: DetailedCashgameAction) => format.hourMinute(action.time);
const getFormattedAmount = (amount: number) => format.currency(amount, props.localization);

const getTypeIcon = (action: DetailedCashgameAction): string => {
  if (action.type === DetailedCashgameResponseActionType.Buyin) return 'pi pi-arrow-circle-right';
  if (action.type === DetailedCashgameResponseActionType.Cashout) return 'pi pi-money-bill';
  return 'pi pi-bars';
};

const getTypeName = (action: DetailedCashgameAction): string => {
  if (action.type === DetailedCashgameResponseActionType.Buyin) return 'Buyin';
  if (action.type === DetailedCashgameResponseActionType.Cashout) return 'Cashout';
  return 'Report';
};

const onRowEditInit = (event: DataTableRowEditInitEvent<DetailedCashgameAction>) => {
  editingRows.value = [event.data];
};

const onRowEditcomplete = (event: DataTableRowEditSaveEvent<DetailedCashgameAction>) => {
  const data: SaveActionEmitData = {
    id: event.newData.id,
    time: event.newData.time,
    stack: event.newData.stack,
    added: event.newData.added,
  };
  console.log('save', data);
  emit('saveAction', data);
  editingRows.value = [];
};

const onRowEditCancel = (event: DataTableRowEditCancelEvent<DetailedCashgameAction>) => {
  editingRows.value = [];
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

<style lang="scss" scoped>
.actions-column {
  display: flex;
  gap: 0.5rem;
  justify-content: right;
}
</style>
