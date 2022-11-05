<template>
  <TableListColumnHeader>
    <CustomLink :url="url">{{ displayDate }}</CustomLink>
  </TableListColumnHeader>
</template>

<script setup lang="ts">
import CustomLink from '@/components/Common/CustomLink.vue';
import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';
import urls from '@/urls';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import format from '@/format';
import { computed } from 'vue';

const props = defineProps<{
  game: ArchiveCashgame;
  slug: string;
}>();

const url = computed(() => {
  return urls.cashgame.details(props.slug, props.game.id);
});

const displayDate = computed(() => {
  return format.monthDay(props.game.startTime);
});
</script>
