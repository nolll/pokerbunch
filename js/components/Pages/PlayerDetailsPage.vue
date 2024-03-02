<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <template v-slot:default>
          <Block>
            <PageHeading :text="playerName" />
          </Block>
          <Block>
            <h2>Player Facts</h2>
          </Block>
          <Block>
            <ValueList>
              <ValueListKey>Total Result</ValueListKey>
              <ValueListValue><WinningsText :value="totalResult" /></ValueListValue>
              <ValueListKey>Best Result</ValueListKey>
              <ValueListValue><WinningsText :value="bestResult" /></ValueListValue>
              <ValueListKey>Worst Result</ValueListKey>
              <ValueListValue><WinningsText :value="worstResult" /></ValueListValue>
              <ValueListKey>Games Played</ValueListKey>
              <ValueListValue>{{ gamesPlayed }}</ValueListValue>
              <ValueListKey>Time Played</ValueListKey>
              <ValueListValue><DurationText :value="timePlayed" /></ValueListValue>
              <ValueListKey>Total Wins</ValueListKey>
              <ValueListValue>{{ totalWins }}</ValueListValue>
              <ValueListKey>Current Streak</ValueListKey>
              <ValueListValue>{{ formattedCurrentStreak }}</ValueListValue>
              <ValueListKey>Best Winning Streak</ValueListKey>
              <ValueListValue>{{ formattedWinningStreak }}</ValueListValue>
              <ValueListKey>Worst Losing Streak</ValueListKey>
              <ValueListValue>{{ formattedLosingStreak }}</ValueListValue>
            </ValueList>
          </Block>
        </template>

        <template v-slot:aside2>
          <Block>
            <h2>User</h2>
          </Block>
          <Block v-if="hasUser">
            <!-- <p>
              <img :src="avatarUrl" alt="User avatar" />
            </p> -->
            <p>This player is a registered user.</p>
            <p>
              <CustomButton :url="userUrl" text="View User Profile" />
            </p>
          </Block>
          <Block v-else>
            <template v-if="isInvitationFormVisible">
              <div class="field">
                <label class="label" for="inviteEmail">Email</label>
                <input class="textfield" v-model="inviteEmail" id="inviteEmail" type="email" />
              </div>
              <div class="buttons">
                <CustomButton @click="invitePlayer" text="Invite" type="action" />
                <CustomButton @click="cancelInvitation" text="Cancel" />
              </div>
            </template>
            <template v-else>
              <p>
                {{ notRegisteredMessage }}
              </p>
              <p v-if="!invitationSent">
                <CustomButton @click="showInvitationForm" text="Invite Player" type="action" />
              </p>
            </template>
          </Block>
        </template>
      </PageSection>

      <PageSection v-if="canDelete">
        <Block>
          <h2>Delete Player</h2>
        </Block>
        <Block>
          <p>
            <CustomButton @click="deletePlayer" text="Delete Player" type="action" />
          </p>
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import urls from '@/urls';
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import ValueList from '@/components/Common/ValueList/ValueList.vue';
import ValueListKey from '@/components/Common/ValueList/ValueListKey.vue';
import ValueListValue from '@/components/Common/ValueList/ValueListValue.vue';
import WinningsText from '@/components/Common/WinningsText.vue';
import DurationText from '@/components/Common/DurationText.vue';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import api from '@/api';
import { User } from '@/models/User';
import useBunches from '@/composables/useBunches';
import useGameArchive from '@/composables/useGameArchive';
import usePlayers from '@/composables/usePlayers';
import { computed, onMounted, ref, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import usePlayerList from '@/composables/usePlayerList';

const route = useRoute();
const router = useRouter();
const bunches = useBunches();
const gameArchive = useGameArchive();
const playersOld = usePlayers();
const { getPlayer, playersReady } = usePlayerList(route.params.slug as string);

const user = ref<User>();
const isInvitationFormVisible = ref(false);
const inviteEmail = ref('');
const invitationSent = ref(false);

const hasUser = computed(() => {
  return !!player.value?.userId;
});

const player = computed(() => {
  return getPlayer(route.params.id as string);
});

const playerName = computed(() => {
  return player.value?.name;
});

const inviteUrl = computed(() => {
  if (!player.value) return null;

  return urls.player.invite(player.value.id);
});

const userUrl = computed(() => {
  if (user.value) return urls.user.details(user.value.userName);
});

const avatarUrl = computed(() => {
  return user.value?.avatar;
});

const games = computed(() => {
  return gameArchive.games.value.filter((g) => isInGame(g));
});

const results = computed(() => {
  let results = [];
  for (const game of games.value) {
    for (const p of game.players) {
      if (p.id === player.value.id) {
        results.push(p);
        break;
      }
    }
  }
  return results;
});

const totalResult = computed(() => {
  return results.value.reduce((acc, cur) => acc + cur.winnings, 0);
});

const bestResult = computed(() => {
  let best: number | null = null;
  for (const result of results.value) {
    if (best == null || result.winnings > best) best = result.winnings;
  }
  return best ?? 0;
});

const worstResult = computed(() => {
  let worst: number | null = null;
  for (const result of results.value) {
    if (worst == null || result.winnings < worst) worst = result.winnings;
  }
  return worst ?? 0;
});

const gamesPlayed = computed(() => {
  return games.value.length;
});

const timePlayed = computed(() => {
  return results.value.reduce((acc, cur) => acc + cur.timePlayed, 0);
});

const totalWins = computed(() => {
  let count = 0;
  for (const game of games.value) {
    if (game.isBestPlayer(player.value.id)) count += 1;
  }
  return count;
});

const currentStreak = computed(() => {
  let lastStreak = 0;
  let currentStreak = 0;
  for (var result of results.value) {
    if (result.winnings >= 0) {
      currentStreak++;
    } else {
      currentStreak--;
    }
    if (Math.abs(currentStreak) < Math.abs(lastStreak)) {
      return lastStreak;
    }
    lastStreak = currentStreak;
  }
  return lastStreak;
});

const bestWinningStreak = computed(() => {
  let bestStreak = 0;
  let currentStreak = 0;
  for (const result of results.value) {
    if (result.winnings >= 0) {
      currentStreak++;
      if (currentStreak > bestStreak) {
        bestStreak = currentStreak;
      }
    } else {
      currentStreak = 0;
    }
  }
  return bestStreak;
});

const formattedCurrentStreak = computed(() => {
  if (currentStreak.value === 0) return '-';

  const wonOrLost = currentStreak.value > 0 ? 'Won' : 'Lost';
  const streak = Math.abs(currentStreak.value);
  return formatStreak(wonOrLost, streak);
});

const formattedWinningStreak = computed(() => {
  return formatStreak('Won', bestWinningStreak.value);
});

const formattedLosingStreak = computed(() => {
  return formatStreak('Lost', worstLosingStreak.value);
});

const worstLosingStreak = computed(() => {
  let worstStreak = 0;
  let currentStreak = 0;
  for (var result of results.value) {
    if (result.winnings < 0) {
      currentStreak++;
      if (currentStreak > worstStreak) {
        worstStreak = currentStreak;
      }
    } else {
      currentStreak = 0;
    }
  }
  return worstStreak;
});

const ready = computed(() => {
  return playersReady && gameArchive.gamesReady.value;
});

const userReady = computed(() => {
  return user.value != null;
});

const canDelete = computed(() => {
  return results.value.length === 0;
});

const showInvitationForm = () => {
  isInvitationFormVisible.value = true;
};

const hideInvitationForm = () => {
  isInvitationFormVisible.value = false;
};

const invitePlayer = () => {
  api.invitePlayer(player.value.id, { email: inviteEmail.value });
  invitationSent.value = true;
  hideInvitationForm();
};

const cancelInvitation = () => {
  hideInvitationForm();
};

const notRegisteredMessage = computed(() => {
  return invitationSent.value ? 'An invitation was sent.' : 'This player is not registered yet.';
});

const deletePlayer = () => {
  if (window.confirm('Do you want to delete this player?')) {
    playersOld.deletePlayer(player.value);
    router.push(urls.player.list(bunches.slug.value));
  }
};

const formatStreak = (wonOrLost: string, gameCount: number) => {
  const gamesText = formatStreakGames(gameCount);
  return `${wonOrLost} in ${gameCount} ${gamesText}`;
};

const formatStreakGames = (streak: number) => {
  return streak === 1 ? 'game' : 'games';
};

const isInGame = (game: ArchiveCashgame) => {
  if (!playersReady) return false;
  for (const p of game.players) {
    if (p.id === player.value.id) return true;
  }
  return false;
};

const loadUser = async () => {
  if (player.value?.userName) {
    const response = await api.getUser(player.value.userName);
    user.value = response.status === 200 ? response.data : undefined;
  }
};

const init = async () => {
  bunches.loadBunch();
  gameArchive.loadGames();
};

// watch(player, () => {
//   if (player.value) loadUser();
// });

onMounted(() => {
  init();
});
</script>
