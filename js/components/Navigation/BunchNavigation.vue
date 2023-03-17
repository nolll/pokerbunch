<template>
  <nav class="game-nav" v-if="hasSlug">
    <h2>
      <CustomLink :url="bunchDetailsUrl">{{ bunchName }}</CustomLink>
    </h2>
    <ul>
      <li>
        <CustomLink :url="cashgamesUrl"><span>Cashgames</span></CustomLink>
      </li>
      <li>
        <CustomLink :url="playersUrl"><span>Players</span></CustomLink>
      </li>
      <li>
        <CustomLink :url="eventsUrl"><span>Events</span></CustomLink>
      </li>
      <li>
        <CustomLink :url="locationsUrl"><span>Locations</span></CustomLink>
      </li>
    </ul>
  </nav>
</template>

<script setup lang="ts">
import CustomLink from '@/components/Common/CustomLink.vue';
import useBunches from '@/composables/useBunches';
import urls from '@/urls';
import { computed } from 'vue';
import { useBunchQuery } from '@/composables/bunchQueries';
import useParams from '@/composables/useParams';

const bunches = useBunches();
const params = useParams();
const bunchQuery = useBunchQuery(params.slug.value);

const slug = computed(() => {
  if (params.slug.value && params.slug.value.length > 0) return params.slug.value;
  if (bunches.userBunches.value.length > 0) return bunches.userBunches.value[0].id;
  return '';
});

const bunchName = computed(() => {
  if (bunchQuery.data.value?.name && bunchQuery.data.value?.name.length > 0) return bunchQuery.data.value?.name;
  if (bunches.userBunches.value.length > 0) return bunches.userBunches.value[0].name;
  return '';
});

const hasSlug = computed(() => {
  return !!slug.value;
});

const bunchDetailsUrl = computed(() => {
  return urls.bunch.details(slug.value);
});

const cashgamesUrl = computed(() => {
  return urls.cashgame.index(slug.value);
});

const playersUrl = computed(() => {
  return urls.player.list(slug.value);
});

const eventsUrl = computed(() => {
  return urls.event.list(slug.value);
});

const locationsUrl = computed(() => {
  return urls.location.list(slug.value);
});
</script>
