<template>
  <div class="matrix" v-if="ready">
    <TableList>
      <thead>
        <tr>
          <TableListColumnHeader />
          <TableListColumnHeader>Player</TableListColumnHeader>
          <TableListColumnHeader>Total</TableListColumnHeader>
          <TableListColumnHeader>
            <CustomLink :url="url">Last game</CustomLink>
          </TableListColumnHeader>
        </tr>
      </thead>
      <tbody>
        <OverviewRow v-for="(player, index) in players" :player="player" :index="index" :key="player.id" :bunchId="slug" />
      </tbody>
    </TableList>
  </div>
</template>

<script setup lang="ts">
import urls from '@/urls';
import OverviewRow from '@/components/Overview/OverviewRow.vue';
import CustomLink from '@/components/Common/CustomLink.vue';
import TableList from '@/components/Common/TableList/TableList.vue';
import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';
import useGameArchive from '@/composables/old/useGameArchive';
import { computed } from 'vue';
import useParams from '@/composables/useParams';

const params = useParams();
const gameArchive = useGameArchive();

const players = computed(() => {
  return gameArchive.currentYearPlayers.value;
});

const url = computed(() => {
  return urls.cashgame.details(slug.value, lastGame.value.id);
});

const lastGame = computed(() => {
  return gameArchive.currentYearGames.value[0];
});

const slug = computed(() => {
  return params.slug.value;
});

const ready = computed(() => {
  return gameArchive.currentYearPlayers.value.length > 0;
});
</script>
