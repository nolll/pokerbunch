<template>
  <div>
    <Block>
      <h1 class="module-heading">Current Game</h1>
      <p>{{ description }}</p>
    </Block>
    <Block>
      <CustomButton :url="url" :text="linkText" type="action" />
    </Block>
  </div>
</template>

<script setup lang="ts">
import urls from '@/urls';
import { computed } from 'vue';
import { Block, CustomButton } from '../Common';
import { useParams } from '@/composables';
import { CurrentGameResponse } from '@/response/CurrentGameResponse';

const props = defineProps<{
  games: CurrentGameResponse[];
}>();

const { slug } = useParams();

const url = computed(() => {
  return gameIsRunning.value ? runningGameUrl.value : addGameUrl.value;
});

const addGameUrl = computed(() => {
  return urls.cashgame.add(slug.value);
});

const runningGameUrl = computed(() => {
  return urls.cashgame.details(slug.value, runningGameId.value);
});

const runningGameId = computed(() => {
  if (props.games.length === 0) return '0';
  return props.games[0].id;
});

const gameIsRunning = computed(() => {
  return props.games.length > 0;
});

const linkText = computed((): string => {
  return gameIsRunning.value ? 'Go to game' : 'Start a game';
});

const description = computed((): string => {
  return gameIsRunning.value ? 'There is a game running' : 'No game is running at the moment';
});
</script>
