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
import CustomLink from '@/components/Common/CustomLink.vue';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import format from '@/format';
import TableListRow from '@/components/Common/TableList/TableListRow.vue';
import TableListCell from '@/components/Common/TableList/TableListCell.vue';
import { computed } from 'vue';
import useBunches from '@/composables/useBunches';
import useFormatter from '@/composables/useFormatter';

const props = defineProps<{
  game: ArchiveCashgame;
}>();

const bunches = useBunches();
const formatter = useFormatter();

const url = computed(() => {
  return urls.cashgame.details(bunches.slug.value, props.game.id);
});

const displayDate = computed(() => {
  return format.monthDay(props.game.date);
});

const duration = computed(() => {
  return formatter.formatDuration(props.game.duration);
});

const formattedAverageBuyin = computed(() => {
  return formatter.formatCurrency(props.game.averageBuyin);
});

const formattedTurnover = computed(() => {
  return formatter.formatCurrency(props.game.turnover);
});
</script>
