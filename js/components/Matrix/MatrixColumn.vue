﻿<template>
  <TableListColumnHeader>
    <CustomLink :url="url">{{ displayDate }}</CustomLink>
  </TableListColumnHeader>
</template>

<script setup lang="ts">
import { CustomLink } from '@/components/Common';
import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';
import urls from '@/urls';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import format from '@/format';
import { computed } from 'vue';

const props = defineProps<{
  game: ArchiveCashgame;
  slug: string;
}>();

const url = computed(() => urls.cashgame.details(props.slug, props.game.id));
const displayDate = computed(() => format.monthDay(props.game.startTime));
</script>
