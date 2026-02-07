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
              <ValueListValue
                ><WinningsText :value="totalResult" :show-currency="true" :localization="localization"
              /></ValueListValue>
              <ValueListKey>Best Result</ValueListKey>
              <ValueListValue
                ><WinningsText :value="bestResult" :show-currency="true" :localization="localization"
              /></ValueListValue>
              <ValueListKey>Worst Result</ValueListKey>
              <ValueListValue
                ><WinningsText :value="worstResult" :show-currency="true" :localization="localization"
              /></ValueListValue>
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
          <Block v-if="isUser">
            <p>This player is a registered user.</p>
            <p>
              <CustomButton :url="userUrl" text="View User Profile" />
            </p>
          </Block>
        </template>
      </PageSection>

      <PageSection v-if="canDelete">
        <Block>
          <h2>Delete Player</h2>
        </Block>
        <Block>
          <ErrorMessage :message="errorMessage" />
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
import { Layout } from '@/components/Layouts';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import { Block, CustomButton, DurationText, ErrorMessage, PageHeading, PageSection, WinningsText } from '@/components/Common';
import { ValueList, ValueListKey, ValueListValue } from '@/components/Common/ValueList';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import api from '@/api';
import { computed, ref } from 'vue';
import { useRouter } from 'vue-router';
import { usePlayerList, useGameList, useParams, useBunch } from '@/composables';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import { playerListKey } from '@/queries/queryKeys';

const { slug, playerId } = useParams();
const router = useRouter();
const { localization, bunchReady } = useBunch(slug.value);
const { getPlayer, tryGetPlayer, playersReady } = usePlayerList(slug.value);
const { allGames, gamesReady } = useGameList(slug.value);
const queryClient = useQueryClient();

const errorMessage = ref('');

const userName = computed(() => tryGetPlayer(playerId.value)?.userName ?? '');
const isUser = computed(() => Boolean(userName.value));
const player = computed(() => getPlayer(playerId.value));
const playerName = computed(() => player.value?.name ?? '');

const userUrl = computed(() => {
  if (userName.value) return urls.user.details(userName.value);
});

const games = computed(() => allGames.value.filter((g) => isInGame(g)));

const results = computed(() => {
  let results = [];
  for (const game of games.value) {
    for (const p of game.players) {
      if (p.id === player.value?.id) {
        results.push(p);
        break;
      }
    }
  }
  return results;
});

const totalResult = computed(() => results.value.reduce((acc, cur) => acc + cur.winnings, 0));

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

const gamesPlayed = computed(() => games.value.length);

const timePlayed = computed(() => {
  return results.value.reduce((acc, cur) => acc + cur.timePlayed, 0);
});

const totalWins = computed(() => {
  let count = 0;
  for (const game of games.value) {
    if (game.isBestPlayer(player.value?.id ?? '')) count += 1;
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

const formattedWinningStreak = computed(() => formatStreak('Won', bestWinningStreak.value));
const formattedLosingStreak = computed(() => formatStreak('Lost', worstLosingStreak.value));

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

const ready = computed(() => bunchReady.value && playersReady.value && gamesReady.value);
const canDelete = computed(() => results.value.length === 0);

const deleteMutation = useMutation({
  mutationFn: async () => {
    await api.deletePlayer(playerId.value);
  },
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: playerListKey(slug.value) });
    router.push(urls.player.list(slug.value));
  },
  onError: () => {
    errorMessage.value = 'Server error';
  },
});

const deletePlayer = () => {
  if (window.confirm('Do you want to delete this player?')) {
    deleteMutation.mutate();
  }
};

const formatStreak = (wonOrLost: string, gameCount: number) => {
  const gamesText = formatStreakGames(gameCount);
  return `${wonOrLost} in ${gameCount} ${gamesText}`;
};

const formatStreakGames = (streak: number) => (streak === 1 ? 'game' : 'games');

const isInGame = (game: ArchiveCashgame) => {
  if (!playersReady.value) return false;
  for (const p of game.players) {
    if (p.id === player.value?.id) return true;
  }
  return false;
};
</script>
