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
import { CustomLink } from '@/components/Common';
import { useBunch, useParams, useUserBunchList, useCurrentUser } from '@/composables';
import urls from '@/urls';
import { computed } from 'vue';

const { slug } = useParams();
const { bunch, bunchReady } = useBunch(slug.value);
const { isSignedIn } = useCurrentUser('');
const { userBunches, userBunchesReady } = useUserBunchList(isSignedIn.value);

const calculatedSlug = computed(() => {
  if (slug.value) return slug.value;
  if (userBunchesReady.value && userBunches.value.length > 0) return userBunches.value[0].id;
  return '';
});

const bunchName = computed(() => {
  if (slug.value && bunchReady.value && bunch.value.name.length > 0) return bunch.value.name;
  if (userBunchesReady.value && userBunches.value.length > 0) return userBunches.value[0].name;
  return '';
});

const hasSlug = computed(() => Boolean(calculatedSlug.value));

const bunchDetailsUrl = computed(() => urls.bunch.details(calculatedSlug.value));
const cashgamesUrl = computed(() => urls.cashgame.index(calculatedSlug.value));
const playersUrl = computed(() => urls.player.list(calculatedSlug.value));
const eventsUrl = computed(() => urls.event.list(calculatedSlug.value));
const locationsUrl = computed(() => urls.location.list(calculatedSlug.value));
</script>
