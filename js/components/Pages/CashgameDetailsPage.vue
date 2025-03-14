<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <template v-slot:default>
          <Block>
            <PageHeading :text="title" />
          </Block>
          <Block v-if="areButtonsVisible">
            <GameButton text="Report" icon="reorder" v-if="canReport" @click.native="showReportForm">
              <ReportIcon />
            </GameButton>
            <GameButton text="Buy In" icon="money" v-if="canBuyin" @click.native="showBuyinForm">
              <BuyinIcon />
            </GameButton>
            <GameButton text="Cash Out" icon="signout" v-if="canCashout" @click.native="showCashoutForm">
              <CashoutIcon />
            </GameButton>
          </Block>
          <Block>
            <ReportForm v-if="reportFormVisible" :suggestedReport="suggestedReport" @report="report" @cancel="hideForms" />
            <BuyinForm
              v-if="buyinFormVisible"
              :suggestedBuyin="suggestedBuyin"
              @buyin="buyin"
              @cancel="hideForms"
              :isPlayerInGame="isInGame"
            />
            <CashoutForm v-if="cashoutFormVisible" :suggestedCashout="suggestedCashout" @cashout="cashout" @cancel="hideForms" />
          </Block>
          <Block v-if="hasPlayers">
            <div class="standings">
              <PlayerTable
                :players="playersInGame"
                :isCashgameRunning="isRunning"
                @playerSelected="onSelectPlayer"
                @deleteAction="onDeleteAction"
                @saveAction="onSaveAction"
                :canEdit="canEdit"
                :bunchId="slug"
                :localization="localization"
              />
            </div>
          </Block>
          <Block v-else> No one has joined the game yet. </Block>
        </template>
        <template v-slot:aside2>
          <Block>
            <ValueList>
              <ValueListKey v-if="showStartTime">Start Time</ValueListKey>
              <ValueListValue v-if="showStartTime">{{ formattedStartTime }}</ValueListValue>
              <ValueListKey v-if="showEndTime">End Time</ValueListKey>
              <ValueListValue v-if="showEndTime">{{ formattedEndTime }}</ValueListValue>
              <ValueListKey v-if="showDuration">Duration</ValueListKey>
              <ValueListValue v-if="showDuration"><DurationText :value="durationMinutes" /></ValueListValue>
              <ValueListKey>Location</ValueListKey>
              <ValueListValue>
                <LocationDropdown v-if="isEditing" :locations="locations" v-model="locationId" />
                <CustomLink v-else :url="locationUrl">{{ locationName }}</CustomLink>
              </ValueListValue>
              <ValueListKey v-if="isPartOfEvent || isEditing">Event</ValueListKey>
              <ValueListValue v-if="isPartOfEvent || isEditing">
                <EventDropdown v-if="isEditing" :events="events" v-model="eventId" />
                <CustomLink v-else :url="eventUrl">{{ eventName }}</CustomLink>
              </ValueListValue>
              <ValueListKey v-if="isPlayerSelectionEnabled">Player</ValueListKey>
              <ValueListValue v-if="isPlayerSelectionEnabled"
                ><PlayerDropdown :players="allPlayers" v-model="selectedPlayerId"
              /></ValueListValue>
            </ValueList>
          </Block>
          <Block v-if="canEdit">
            <template v-if="isEditing">
              <CustomButton @click="onSave" type="action" text="Save" />
              <CustomButton @click="onCancelEdit" text="Cancel" />
            </template>
            <CustomButton v-else @click="onEdit" type="action" text="Edit Cashgame" />
          </Block>
          <Block v-if="isEditing && !hasPlayers">
            <CustomButton @click="onDelete" type="action" text="Delete" />
          </Block>
        </template>
      </PageSection>

      <PageSection v-if="hasPlayers">
        <Block>
          <GameChart :players="playersInGame" />
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import urls from '@/urls';
import timeFunctions from '@/time-functions';
import { Layout } from '@/components/Layouts';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import GameButton from '@/components/CurrentGame/GameButton.vue';
import ReportForm from '@/components/CurrentGame/ReportForm.vue';
import BuyinForm from '@/components/CurrentGame/BuyinForm.vue';
import CashoutForm from '@/components/CurrentGame/CashoutForm.vue';
import PlayerDropdown from '@/components/PlayerDropdown.vue';
import PlayerTable from '@/components/CurrentGame/PlayerTable.vue';
import GameChart from '@/components/CurrentGame/GameChart.vue';
import { Block, CustomButton, CustomLink, DurationText, PageHeading, PageSection } from '@/components/Common';
import { ValueList, ValueListKey, ValueListValue } from '@/components/Common/ValueList';
import LocationDropdown from '@/components/LocationDropdown.vue';
import EventDropdown from '@/components/EventDropdown.vue';
import format from '@/format';
import dayjs from 'dayjs';
import api from '@/api';
import { computed, ref, watch } from 'vue';
import { useRouter } from 'vue-router';
import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
import { BuyinIcon, CashoutIcon, ReportIcon } from '../Icons';
import { useParams, useLocationList, useBunch, usePlayerList, useEventList, useGame } from '@/composables';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import { gameKey, gameListKey } from '@/queries/queryKeys';
import { SaveActionEmitData } from '@/models/SaveActionEmitData';
import { DetailedCashgameResponseActionType } from '@/response/DetailedCashgameResponseActionType';

const { slug, cashgameId } = useParams();
const router = useRouter();
const { bunch, localization, isManager, bunchReady } = useBunch(slug.value);
const { players, playersReady } = usePlayerList(slug.value);
const { locations, locationsReady } = useLocationList(slug.value);
const { events, eventsReady } = useEventList(slug.value);

const reportFormVisible = ref(false);
const buyinFormVisible = ref(false);
const cashoutFormVisible = ref(false);
const selectedPlayerId = ref('');
const isEditing = ref(false);
const locationId = ref<string>();
const eventId = ref<string>();

const { game: cashgame, gameReady } = useGame(cashgameId.value);
const queryClient = useQueryClient();

const title = computed(() => `Cashgame ${formattedDate.value}`);
const formattedDate = computed(() => format.monthDayYear(startTime.value));
const formattedStartTime = computed(() => format.hourMinute(startTime.value));
const formattedEndTime = computed(() => {
  if (!cashgame.value) return '';
  return format.hourMinute(cashgame.value.updatedTime);
});

const durationMinutes = computed(() => {
  if (!cashgame.value) return 0;
  return timeFunctions.diffInMinutes(startTime.value, cashgame.value.updatedTime);
});

const showStartTime = computed(() => hasPlayers.value);
const showEndTime = computed(() => isEnded.value);
const showDuration = computed(() => isEnded.value);
const isRunning = computed(() => !!(cashgame.value?.isRunning ?? false));
const isInGame = computed(() => !!(playerInGame.value ?? false));
const canReport = computed(() => isInGame.value && !hasCachedOut.value);
const canBuyin = computed(() => !hasCachedOut.value);
const canCashout = computed(() => isInGame.value);

const hasCachedOut = computed(() => {
  if (!playerInGame.value) return false;
  return playerInGame.value.hasCashedOut();
});

const isEnded = computed(() => hasPlayers.value && !isRunning.value);
const areButtonsVisible = computed(() => isRunning.value && !isAnyFormVisible.value);
const isAnyFormVisible = computed(
  () => (isRunning.value && reportFormVisible.value) || buyinFormVisible.value || cashoutFormVisible.value
);
const isPlayerSelectionEnabled = computed(() => isRunning.value && isManager.value && !isEditing.value);
const locationName = computed(() => cashgame.value?.location.name || '');

const locationUrl = computed(() => {
  if (!cashgame.value) return '';
  return urls.location.details(slug.value, cashgame.value.location.id);
});

const isPartOfEvent = computed(() => Boolean(cashgame.value?.event));
const eventName = computed(() => cashgame.value?.event?.name || '');

const eventUrl = computed(() => {
  if (!cashgame.value) return '';

  if (!cashgame.value.event) return '';

  return urls.event.details(slug.value, cashgame.value.event.id);
});

const canEdit = computed((): boolean => isManager.value);

const playersInGame = computed((): DetailedCashgamePlayer[] => {
  if (!cashgame.value) return [];
  return cashgame.value.players.slice().sort((left, right) => right.getWinnings() - left.getWinnings());
});

const allPlayers = computed(() => players.value);
const hasPlayers = computed(() => Boolean(playersInGame.value.length));

const suggestedBuyin = computed(() => {
  var p = playerInGame.value;
  if (p === null) return bunch.value.defaultBuyin;

  var buyins = p.buyins();
  if (buyins.length === 0) return bunch.value.defaultBuyin;

  return buyins[buyins.length - 1].added ?? 0;
});

const suggestedReport = computed(() => {
  var p = playerInGame.value;
  if (p === null) return 0;

  var reports = p.reports();
  if (reports.length === 0) return suggestedBuyin.value;

  return reports[reports.length - 1].stack;
});

const suggestedCashout = computed(() => {
  var p = playerInGame.value;
  if (p === null) return 0;

  var cashouts = p.cashouts();
  if (cashouts.length === 0) return suggestedReport.value;

  return cashouts[cashouts.length - 1].stack;
});

const playerInGame = computed(() => getPlayerInGame(selectedPlayerId.value));

const startTime = computed(() => {
  let first;
  let t = dayjs().utc();
  const p = playersInGame.value;

  if (p.length === 0) return t.toDate();
  for (let i = 0; i < p.length; i++) {
    first = p[i].actions[0];
    if (first) {
      const firstTime = dayjs(first.time);
      if (firstTime.isBefore(t)) {
        t = firstTime;
      }
    }
  }
  return t.toDate();
});

const updatedTime = computed(() => cashgame.value?.updatedTime || null);
const ready = computed(
  () => bunchReady.value && gameReady.value && playersReady.value && locationsReady.value && eventsReady.value
);

const report = async (stack: number) => {
  reportMutation.mutate({ stack });
};

const reportMutation = useMutation({
  mutationFn: async (params: { stack: number }) => {
    const reportData = { type: 'report', playerId: selectedPlayerId.value, stack: params.stack };
    await api.report(cashgame.value.id, reportData);
  },
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: gameKey(cashgameId.value) });
    resetSelectedPlayerId();
    hideForms();
  },
});

const buyin = async (amount: number, stack: number) => {
  buyinMutation.mutate({ stack: stack, added: amount });
};

const buyinMutation = useMutation({
  mutationFn: async (params: { stack: number; added: number }) => {
    const buyinData = { type: 'buyin', playerId: selectedPlayerId.value, stack: params.stack, added: params.added };
    await api.buyin(cashgame.value.id, buyinData);
  },
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: gameKey(cashgameId.value) });
    resetSelectedPlayerId();
    hideForms();
  },
});

const cashout = async (stack: number) => {
  cashoutMutation.mutate({ stack });
};

const cashoutMutation = useMutation({
  mutationFn: async (params: { stack: number }) => {
    const cashoutData = { type: 'cashout', playerId: selectedPlayerId.value, stack: params.stack };
    await api.cashout(cashgame.value.id, cashoutData);
  },
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: gameKey(cashgameId.value) });
    resetSelectedPlayerId();
    hideForms();
  },
});

const showReportForm = () => {
  reportFormVisible.value = true;
};

const showBuyinForm = () => {
  buyinFormVisible.value = true;
};

const showCashoutForm = () => {
  cashoutFormVisible.value = true;
};

const hideForms = () => {
  reportFormVisible.value = false;
  buyinFormVisible.value = false;
  cashoutFormVisible.value = false;
};

const resetSelectedPlayerId = () => {
  selectedPlayerId.value = bunch.value.player.id;
};

const getPlayerInGame = (id: string) => {
  if (!id) return null;
  return playersInGame.value.find((p) => p.id.toString() === id.toString()) || null;
};

const onSelectPlayer = (id: string) => {
  if (isPlayerSelectionEnabled.value) selectedPlayerId.value = id;
};

const onEdit = () => {
  isEditing.value = true;
};

const onSave = async () => {
  if (!cashgame.value || !locationId.value) return;
  saveMutation.mutate();
};

const saveMutation = useMutation({
  mutationFn: async () => {
    await api.updateCashgame(cashgameId.value, {
      locationId: locationId.value,
      eventId: eventId.value !== '' ? eventId.value : null,
    });
  },
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: gameKey(cashgameId.value) });
    isEditing.value = false;
  },
});

const onDelete = async () => {
  if (!cashgame.value || hasPlayers.value) return;
  deleteMutation.mutate();
};

const deleteMutation = useMutation({
  mutationFn: async () => {
    await api.deleteCashgame(cashgameId.value);
  },
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: gameListKey(cashgameId.value) });
    redirect();
  },
});

const onCancelEdit = () => {
  isEditing.value = false;
};

const onDeleteAction = async (id: string) => {
  deleteActionMutation.mutate({ id });
};

const deleteActionMutation = useMutation({
  mutationFn: async (params: { id: string }) => {
    await api.deleteAction(cashgame.value.id, params.id);
  },
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: gameKey(cashgameId.value) });
  },
});

const onSaveAction = async (data: SaveActionEmitData) => {
  updateActionMutation.mutate(data);
};

const updateActionMutation = useMutation({
  mutationFn: async (data: SaveActionEmitData) => {
    const updateData = {
      added: data.added,
      stack: data.stack,
      timestamp: data.time,
    };
    await api.updateAction(cashgame.value.id, data.id, updateData);
  },
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: gameKey(cashgameId.value) });
  },
});

const redirect = () => {
  router.push(urls.cashgame.index(slug.value));
};

const playerId = computed(() => bunch.value.player.id);

watch(cashgame, () => {
  locationId.value = cashgame.value?.location.id || undefined;
  eventId.value = cashgame.value?.event?.id || undefined;
  selectedPlayerId.value = playerId.value;
});

watch(bunch, () => {
  selectedPlayerId.value = bunch.value.player.id;
});
</script>
