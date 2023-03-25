<template>
  <TableListRow>
    <TableListCell>
      <CustomLink :url="url">{{ displayDate }}</CustomLink>
    </TableListCell>
    <TableListCell :is-numeric="true">{{ game.playerCount }}</TableListCell>
    <TableListCell>{{ game.location.name }}</TableListCell>
    <TableListCell><DurationText :value="duration" /></TableListCell>
    <TableListCell :is-numeric="true"><CurrencyText :value="turnover" /></TableListCell>
    <TableListCell :is-numeric="true"><CurrencyText :value="averageBuyin" /></TableListCell>
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
import CurrencyText from '../Common/CurrencyText.vue';
import DurationText from '../Common/DurationText.vue';

const props = defineProps<{
  game: ArchiveCashgame;
  slug: string;
}>();

const url = computed(() => urls.cashgame.details(props.slug, props.game.id));
const displayDate = computed(() => format.monthDay(props.game.date));
const duration = computed(() => props.game.duration);
const averageBuyin = computed(() => props.game.averageBuyin);
const turnover = computed(() => props.game.turnover);
</script>
