<template>
  <TableListRow>
    <TableListCell>
      <CustomLink :url="url">{{ displayDate }}</CustomLink>
    </TableListCell>
    <TableListCell :is-numeric="true">{{ game.playerCount }}</TableListCell>
    <TableListCell>{{ game.location.name }}</TableListCell>
    <TableListCell>{{ duration }}</TableListCell>
    <TableListCell :is-numeric="true">{{ formattedTurnover }}</TableListCell>
    <TableListCell :is-numeric="true">{{ formattedAverageBuyin }}</TableListCell>
  </TableListRow>
</template>

<script setup lang="ts">
import urls from '@/urls';
import { CustomLink } from '@/components/Common';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import format from '@/format';
import { TableListCell, TableListRow } from '@/components/Common/TableList';
import { computed } from 'vue';
import { BunchResponse } from '@/response/BunchResponse';
import { Localization } from '@/models/Localization';

const props = defineProps<{
  bunch: BunchResponse;
  game: ArchiveCashgame;
  localization: Localization;
}>();

const url = computed(() => {
  return urls.cashgame.details(props.bunch.id, props.game.id);
});

const displayDate = computed(() => {
  return format.monthDay(props.game.date);
});

const duration = computed(() => {
  return format.duration(props.game.duration);
});

const formattedAverageBuyin = computed(() => {
  return format.currency(props.game.averageBuyin, props.localization);
});

const formattedTurnover = computed(() => {
  return format.currency(props.game.turnover, props.localization);
});
</script>
