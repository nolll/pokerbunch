<template>
  <div>
    <h2 class="h2">Overall</h2>
    <DefinitionList>
      <DefinitionTerm>Number of games</DefinitionTerm>
      <DefinitionData>{{ gameCount }}</DefinitionData>

      <DefinitionTerm>Total Time Played</DefinitionTerm>
      <DefinitionData><DurationText :value="facts.duration" /></DefinitionData>

      <DefinitionTerm>Total Turnover</DefinitionTerm>
      <DefinitionData><CurrencyText :value="turnover" /></DefinitionData>
    </DefinitionList>
  </div>
</template>

<script setup lang="ts">
import DefinitionList from '@/components/DefinitionList/DefinitionList.vue';
import DefinitionData from '@/components/DefinitionList/DefinitionData.vue';
import DefinitionTerm from '@/components/DefinitionList/DefinitionTerm.vue';
import DurationText from '@/components/Common/DurationText.vue';
import CurrencyText from '@/components/Common/CurrencyText.vue';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { OverallFactCollection } from '@/models/OverallFactCollection';
import { computed } from 'vue';

const props = defineProps<{
  games: ArchiveCashgame[];
}>();

const facts = computed(() => {
  return getFacts(props.games);
});

const gameCount = computed(() => {
  return props.games.length;
});

const turnover = computed(() => {
  return facts.value.turnover;
});

const getFacts = (games: ArchiveCashgame[]): OverallFactCollection => {
  var duration = 0;
  var turnover = 0;
  for (var gi = 0; gi < games.length; gi++) {
    var game = games[gi];
    duration += game.duration;
    turnover += game.turnover;
  }
  return {
    duration: duration,
    turnover: turnover,
  };
};
</script>
