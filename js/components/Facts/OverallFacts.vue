<template>
  <div>
    <h2 class="h2">Overall</h2>
    <DefinitionList>
      <DefinitionTerm>Number of games</DefinitionTerm>
      <DefinitionData>{{ gameCount }}</DefinitionData>

      <DefinitionTerm>Total Time Played</DefinitionTerm>
      <DefinitionData><DurationText :value="facts.duration" /></DefinitionData>

      <DefinitionTerm>Total Turnover</DefinitionTerm>
      <DefinitionData><CurrencyText :value="turnover" :localization="localization" /></DefinitionData>
    </DefinitionList>
  </div>
</template>

<script setup lang="ts">
import { DefinitionList, DefinitionData, DefinitionTerm } from '@/components/Common/DefinitionList';
import { CurrencyText, DurationText } from '@/components/Common';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { OverallFactCollection } from '@/models/OverallFactCollection';
import { computed } from 'vue';
import { Localization } from '@/models/Localization';

const props = defineProps<{
  games: ArchiveCashgame[];
  localization: Localization;
}>();

const facts = computed(() => getFacts(props.games));
const gameCount = computed(() => props.games.length);
const turnover = computed(() => facts.value.turnover);

const getFacts = (games: ArchiveCashgame[]): OverallFactCollection => {
  const totals = games.reduce(
    (acc, game) => ({
      duration: acc.duration + game.duration,
      turnover: acc.turnover + game.turnover,
    }),
    { duration: 0, turnover: 0 },
  );
  return totals;
};
</script>
