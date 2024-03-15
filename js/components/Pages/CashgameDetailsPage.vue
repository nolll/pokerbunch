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
            <ReportForm v-show="reportFormVisible" :defaultBuyin="defaultBuyin" @report="report" @cancel="hideForms" />
            <BuyinForm
              v-show="buyinFormVisible"
              :defaultBuyin="defaultBuyin"
              @buyin="buyin"
              @cancel="hideForms"
              :isPlayerInGame="isInGame"
            />
            <CashoutForm v-show="cashoutFormVisible" :defaultBuyin="defaultBuyin" @cashout="cashout" @cancel="hideForms" />
          </Block>
          <Block v-if="hasPlayers">
            <div class="standings">
              <!-- todo: changed cashgame.players to playersInGame. Check if it is still working -->
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
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import GameButton from '@/components/CurrentGame/GameButton.vue';
import ReportForm from '@/components/CurrentGame/ReportForm.vue';
import BuyinForm from '@/components/CurrentGame/BuyinForm.vue';
import CashoutForm from '@/components/CurrentGame/CashoutForm.vue';
import PlayerDropdown from '@/components/PlayerDropdown.vue';
import PlayerTable from '@/components/CurrentGame/PlayerTable.vue';
import GameChart from '@/components/CurrentGame/GameChart.vue';
import Block from '@/components/Common/Block.vue';
import CustomLink from '@/components/Common/CustomLink.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import ValueList from '@/components/Common/ValueList/ValueList.vue';
import ValueListKey from '@/components/Common/ValueList/ValueListKey.vue';
import ValueListValue from '@/components/Common/ValueList/ValueListValue.vue';
import DurationText from '@/components/Common/DurationText.vue';
import LocationDropdown from '@/components/LocationDropdown.vue';
import EventDropdown from '@/components/EventDropdown.vue';
import format from '@/format';
import dayjs from 'dayjs';
import { DetailedCashgame } from '@/models/DetailedCashgame';
import api from '@/api';
import { DetailedCashgameLocation } from '@/models/DetailedCashgameLocation';
import { DetailedCashgameEvent } from '@/models/DetailedCashgameEvent';
import { computed, onBeforeUnmount, onMounted, ref, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
import ReportIcon from '../Icons/ReportIcon.vue';
import BuyinIcon from '../Icons/BuyinIcon.vue';
import CashoutIcon from '../Icons/CashoutIcon.vue';
import useParams from '@/composables/useParams';
import useLocationList from '@/composables/useLocationList';
import useBunch from '@/composables/useBunch';
import usePlayerList from '@/composables/usePlayerList';
import useEventList from '@/composables/useEventList';

const { slug } = useParams();
const route = useRoute();
const router = useRouter();
const { bunch, localization, isManager, bunchReady } = useBunch(slug.value);
const { players, getPlayer, playersReady } = usePlayerList(slug.value);
const { locations, getLocation, locationsReady } = useLocationList(slug.value);
const { events, getEvent, eventsReady } = useEventList(slug.value);

const longRefresh = 30000;

const cashgame = ref<DetailedCashgame | null>(null);
const reportFormVisible = ref(false);
const buyinFormVisible = ref(false);
const cashoutFormVisible = ref(false);
const selectedPlayerId = ref('');
const isEditing = ref(false);
const locationId = ref<string>();
const eventId = ref<string>();
const refreshHandle = ref(0);

const title = computed(() => {
  return `Cashgame ${formattedDate.value}`;
});

const formattedDate = computed(() => {
  return format.monthDayYear(startTime.value);
});

const formattedStartTime = computed(() => {
  return format.hourMinute(startTime.value);
});

const formattedEndTime = computed(() => {
  if (!cashgame.value) return '';
  return format.hourMinute(cashgame.value.updatedTime);
});

const durationMinutes = computed(() => {
  if (!cashgame.value) return 0;
  return timeFunctions.diffInMinutes(startTime.value, cashgame.value.updatedTime);
});

const showStartTime = computed(() => {
  return hasPlayers.value;
});

const showEndTime = computed(() => {
  return isEnded.value;
});

const showDuration = computed(() => {
  return isEnded.value;
});

const isRunning = computed(() => {
  return !!cashgame.value?.isRunning;
});

const isInGame = computed(() => {
  return !!playerInGame.value;
});

const canReport = computed(() => {
  return isInGame.value && !hasCachedOut.value;
});

const canBuyin = computed(() => {
  return !hasCachedOut.value;
});

const canCashout = computed(() => {
  return isInGame.value;
});

const hasCachedOut = computed(() => {
  if (!playerInGame.value) return false;
  return playerInGame.value.hasCashedOut();
});

const isEnded = computed(() => {
  return hasPlayers.value && !isRunning.value;
});

const areButtonsVisible = computed(() => {
  return isRunning.value && !isAnyFormVisible.value;
});

const isAnyFormVisible = computed(() => {
  return (isRunning.value && reportFormVisible.value) || buyinFormVisible.value || cashoutFormVisible.value;
});

const isPlayerSelectionEnabled = computed(() => {
  return isRunning.value && isManager.value && !isEditing.value;
});

const locationName = computed(() => {
  return cashgame.value?.location.name || '';
});

const locationUrl = computed(() => {
  if (!cashgame.value) return '';
  return urls.location.details(slug.value, cashgame.value.location.id);
});

const isPartOfEvent = computed(() => {
  return !!cashgame.value?.event;
});

const eventName = computed(() => {
  return cashgame.value?.event?.name || '';
});

const eventUrl = computed(() => {
  if (!cashgame.value) return '';

  if (!cashgame.value.event) return '';

  return urls.event.details(slug.value, cashgame.value.event.id);
});

const canEdit = computed((): boolean => {
  return isManager.value;
});

// todo: I think I've found the bug with the sorted players. SortedPlayers is not used
const playersInGame = computed((): DetailedCashgamePlayer[] => {
  if (!cashgame.value) return [];
  const sortedPlayers = cashgame.value.players.slice().sort((left, right) => right.getWinnings() - left.getWinnings());
  return cashgame.value.players;
});

const allPlayers = computed(() => {
  return players.value;
});

const hasPlayers = computed(() => {
  return !!playersInGame.value.length;
});

const userPlayer = computed(() => {
  return getPlayer(bunch.value.player.id);
});

const playerName = computed(() => {
  return player.value?.name || '';
});

const playerColor = computed(() => {
  return player.value?.color || '#9e9e9e';
});

const defaultBuyin = computed(() => {
  return bunch.value.defaultBuyin;
});

const player = computed(() => {
  return getPlayer(selectedPlayerId.value);
});

const playerInGame = computed(() => {
  return getPlayerInGame(selectedPlayerId.value);
});

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

const updatedTime = computed(() => {
  return cashgame.value?.updatedTime || null;
});

const ready = computed(() => {
  return bunchReady.value && cashgameReady.value && playersReady.value && locationsReady.value && eventsReady.value;
});

const setupRefresh = (refreshTimeout: number) => {
  if (isRunning.value) {
    refreshHandle.value = window.setInterval(async () => {
      await refresh();
    }, refreshTimeout);
  }
};

const report = async (stack: number) => {
  if (!cashgame.value) return;

  cashgame.value.report(selectedPlayerId.value, stack);
  const reportData = { type: 'report', playerId: selectedPlayerId.value, stack: stack };
  resetSelectedPlayerId();
  hideForms();
  /*mutate*/ await api.report(cashgame.value.id, reportData);
};

const buyin = async (amount: number, stack: number) => {
  if (!cashgame.value) return;

  if (!isInGame.value) {
    const player = cashgame.value.addPlayer(selectedPlayerId.value, playerName.value, playerColor.value);
  }

  cashgame.value.buyin(selectedPlayerId.value, amount, stack);
  const buyinData = { type: 'buyin', playerId: selectedPlayerId.value, stack: stack, added: amount };
  resetSelectedPlayerId();
  hideForms();
  /*mutate*/ await api.buyin(cashgame.value.id, buyinData);
};

const cashout = async (stack: number) => {
  if (!cashgame.value) return;

  cashgame.value.cashout(selectedPlayerId.value, stack);
  const cashoutData = { type: 'cashout', playerId: selectedPlayerId.value, stack: stack };
  resetSelectedPlayerId();
  hideForms();
  /*mutate*/ await api.cashout(cashgame.value.id, cashoutData);
};

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
  if (!cashgame.value) return;

  if (!locationId.value) return;

  const location = getLocation(locationId.value);
  if (!location) return;

  const cashgameLocation = new DetailedCashgameLocation(location.id, location.name);

  let cashgameEvent: DetailedCashgameEvent | null = null;
  if (eventId.value) {
    const event = getEvent(eventId.value);
    if (event) {
      cashgameEvent = new DetailedCashgameEvent(event.id.toString(), event.name);
    }
  }

  cashgame.value.update(cashgameLocation, cashgameEvent);
  isEditing.value = false;

  /*mutate*/ await api.updateCashgame(cashgame.value.id, {
    locationId: cashgameLocation.id,
    eventId: cashgameEvent?.id,
  });
};

const onDelete = async () => {
  if (!cashgame.value || hasPlayers.value) return;

  /*mutate*/ await api.deleteCashgame(cashgame.value.id);
  redirect();
};

const onCancelEdit = () => {
  isEditing.value = false;
};

const onDeleteAction = async (id: string) => {
  if (!cashgame.value) return;

  cashgame.value.deleteAction(id);
  /*mutate*/ await api.deleteAction(cashgame.value.id, id);
};

const onSaveAction = async (data: any) => {
  if (!cashgame.value) return;

  cashgame.value.updateAction(data.id, data);
  const updateData = {
    added: data.added,
    stack: data.stack,
    timestamp: data.time,
  };
  /*mutate*/ await api.updateAction(cashgame.value.id, data.id, updateData);
};

onMounted(async () => {
  await init();
});

onBeforeUnmount(() => {
  if (refreshHandle.value) window.clearInterval(refreshHandle.value);
});

const redirect = () => {
  router.push(urls.cashgame.index(slug.value));
};

const loadCashgame = async () => {
  const response = /*mutate*/ await api.getCashgame(route.params.id as string);
  cashgame.value = response.status === 200 ? new DetailedCashgame(response.data) : null;
  locationId.value = cashgame.value?.location.id || undefined;
  eventId.value = cashgame.value?.event?.id || undefined;
};

const refresh = async () => {
  await loadCashgame();
};

const cashgameReady = computed(() => {
  return !!cashgame.value;
});

const init = async () => {
  selectedPlayerId.value = bunch.value.player.id;
  await loadCashgame();
  setupRefresh(longRefresh);
};

const playerId = computed(() => {
  return bunch.value.player.id;
});

watch(playerId, () => {
  selectedPlayerId.value = playerId.value;
});
</script>
