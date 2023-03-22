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
import urls from '@/urls';
import { computed } from 'vue';
import { useBunchQuery, useUserBunchesQuery } from '@/queries/bunchQueries';
import useParams from '@/helpers/useParams';
import auth from '@/auth';

const params = useParams();
const selectedSlug = computed(() => params.slug.value);
const hasSelectedSlug = computed(() => !!selectedSlug.value);
const hasSlug = computed(() => !!slug.value);
const bunchDetailsUrl = computed(() => urls.bunch.details(slug.value));
const cashgamesUrl = computed(() => urls.cashgame.index(slug.value));
const playersUrl = computed(() => urls.player.list(slug.value));
const eventsUrl = computed(() => urls.event.list(slug.value));
const locationsUrl = computed(() => urls.location.list(slug.value));
const bunchQuery = useBunchQuery(selectedSlug.value);

const isSignedIn = computed(() => auth.isLoggedIn());
const userBunchesQuery = useUserBunchesQuery(isSignedIn.value);

const slug = computed(() => {
  if (hasSelectedSlug.value) return selectedSlug.value;
  if (isSignedIn.value && userBunchesQuery.data.value && userBunchesQuery.data.value.length > 0)
    return userBunchesQuery.data.value[0].id;
  return '';
});

const bunchName = computed(() => {
  if (params.slug.value && bunchQuery.data.value?.name && bunchQuery.data.value?.name.length > 0)
    return bunchQuery.data.value?.name;
  if (isSignedIn.value && userBunchesQuery.data.value && userBunchesQuery.data.value.length > 0)
    return userBunchesQuery.data.value[0].name;
  return '';
});
</script>
