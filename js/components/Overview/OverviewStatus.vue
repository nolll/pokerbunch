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
import Block from '@/components/Common/Block.vue';
import { computed } from 'vue';
import CustomButton from '../Common/CustomButton.vue';
import { CurrentGameResponse } from '@/response/CurrentGameResponse';
import useParams from '@/helpers/useParams';

const props = defineProps<{
  games: CurrentGameResponse[];
}>();

const params = useParams();

const url = computed(() => (gameIsRunning.value ? runningGameUrl.value : addGameUrl.value));
const addGameUrl = computed(() => urls.cashgame.add(params.slug.value));
const runningGameUrl = computed(() => urls.cashgame.details(params.slug.value, runningGameId.value));

const runningGameId = computed(() => {
  if (props.games.length === 0) return '0';
  return props.games[0].id;
});

const gameIsRunning = computed(() => props.games.length > 0);
const linkText = computed((): string => (gameIsRunning.value ? 'Go to game' : 'Start a game'));
const description = computed((): string => (gameIsRunning.value ? 'There is a game running' : 'No game is running at the moment'));
</script>
