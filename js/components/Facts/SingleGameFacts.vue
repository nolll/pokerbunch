<template>
  <div v-if="ready">
    <h2 class="h2">Single Game</h2>
    <DefinitionList>
      <DefinitionTerm>Best Result</DefinitionTerm>
      <PlayerResultFact :name="facts.bestResult.name" :amount="facts.bestResult.amount" />

      <DefinitionTerm>Worst Result</DefinitionTerm>
      <PlayerResultFact :name="facts.worstResult.name" :amount="facts.worstResult.amount" />
    </DefinitionList>
  </div>
</template>

<script setup lang="ts">
//Things to add
//BiggestBuyin
//BiggestCashout
//BiggestComeback

import PlayerResultFact from './PlayerResultFact.vue';
import DefinitionList from '@/components/DefinitionList/DefinitionList.vue';
import DefinitionTerm from '@/components/DefinitionList/DefinitionTerm.vue';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { SingleGameFactCollection } from '@/models/SingleGameFactCollection';
import { PlayerWinningsFact } from '@/models/PlayerWinningsFact';
import { computed } from 'vue';
import useGameArchive from '@/composables/useGameArchive';
import useBunches from '@/composables/useBunches';

const bunches = useBunches();
const gameArchive = useGameArchive();

const facts = computed(() => {
  return getFacts(gameArchive.sortedPlayers.value);
});

const ready = computed(() => {
  return bunches.bunchReady.value && gameArchive.gamesReady.value;
});

const getFacts = (players: CashgameListPlayerData[]): SingleGameFactCollection => {
  var bestResult: PlayerWinningsFact = { name: '', id: '0', amount: 0 };
  var worstResult: PlayerWinningsFact = { name: '', id: '0', amount: 0 };
  for (var pi = 0; pi < players.length; pi++) {
    var player = players[pi];
    for (var gi = 0; gi < player.gameResults.length; gi++) {
      var game = player.gameResults[gi];
      if (game && game.winnings > bestResult.amount) {
        bestResult = { name: player.name, id: player.id, amount: game.winnings };
      }
      if (game && game.winnings < worstResult.amount) {
        worstResult = { name: player.name, id: player.id, amount: game.winnings };
      }
    }
  }
  return {
    bestResult: bestResult,
    worstResult: worstResult,
  };
};
</script>
