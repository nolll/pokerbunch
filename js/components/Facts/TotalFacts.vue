<template>
  <div>
    <h2 class="h2">Totals</h2>
    <DefinitionList>
      <DefinitionTerm>Most Time Played</DefinitionTerm>
      <PlayerTimeFact :name="facts.mostTime.name" :minutes="facts.mostTime.minutes" />

      <DefinitionTerm>Best Total Result</DefinitionTerm>
      <PlayerResultFact :name="facts.bestTotal.name" :amount="facts.bestTotal.amount" :localization="localization" />

      <DefinitionTerm>Worst Total Result</DefinitionTerm>
      <PlayerResultFact :name="facts.worstTotal.name" :amount="facts.worstTotal.amount" :localization="localization" />

      <DefinitionTerm>Biggest Total Buyin</DefinitionTerm>
      <PlayerAmountFact :name="facts.biggestBuyin.name" :amount="facts.biggestBuyin.amount" :localization="localization" />

      <DefinitionTerm>Biggest Total Cashout</DefinitionTerm>
      <PlayerAmountFact :name="facts.biggestCashout.name" :amount="facts.biggestCashout.amount" :localization="localization" />
    </DefinitionList>
  </div>
</template>

<script setup lang="ts">
//Things to add
//MostGamesPlayed
//HighestWinrate

import PlayerResultFact from './PlayerResultFact.vue';
import PlayerAmountFact from './PlayerAmountFact.vue';
import PlayerTimeFact from './PlayerTimeFact.vue';
import DefinitionList from '@/components/DefinitionList/DefinitionList.vue';
import DefinitionTerm from '@/components/DefinitionList/DefinitionTerm.vue';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { TotalFactCollection } from '@/models/TotalFactCollection';
import { computed } from 'vue';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import archiveHelper from '@/ArchiveHelper';
import playerSorter from '@/PlayerSorter';
import { Localization } from '@/models/Localization';

const props = defineProps<{
  games: ArchiveCashgame[];
  localization: Localization;
}>();

const facts = computed(() => getFacts(players.value));
const players = computed(() => playerSorter.sort(archiveHelper.getPlayers(props.games)));

const getFacts = (players: CashgameListPlayerData[]): TotalFactCollection => {
  var mostTime = { name: '', id: '0', minutes: 0 };
  var bestTotal = { name: '', id: '0', amount: 0 };
  var worstTotal = { name: '', id: '0', amount: 0 };
  var biggestBuyin = { name: '', id: '0', amount: 0 };
  var biggestCashout = { name: '', id: '0', amount: 0 };
  for (var pi = 0; pi < players.length; pi++) {
    var player = players[pi];
    if (player.playedTimeInMinutes > mostTime.minutes) {
      mostTime = { name: player.name, id: player.id, minutes: player.playedTimeInMinutes };
    }
    if (player.winnings > bestTotal.amount) {
      bestTotal = { name: player.name, id: player.id, amount: player.winnings };
    }
    if (player.winnings < worstTotal.amount) {
      worstTotal = { name: player.name, id: player.id, amount: player.winnings };
    }
    if (player.buyin > biggestBuyin.amount) {
      biggestBuyin = { name: player.name, id: player.id, amount: player.buyin };
    }
    if (player.stack > biggestCashout.amount) {
      biggestCashout = { name: player.name, id: player.id, amount: player.stack };
    }
  }
  return {
    mostTime: mostTime,
    bestTotal: bestTotal,
    worstTotal: worstTotal,
    biggestBuyin: biggestBuyin,
    biggestCashout: biggestCashout,
  };
};
</script>
