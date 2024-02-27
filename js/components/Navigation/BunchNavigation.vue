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
import useBunch from '@/composables/useBunch';
import useParams from '@/composables/useParams';
import useUserBunchList from '@/composables/useUserBunchList';
import urls from '@/urls';
import { computed } from 'vue';

const params = useParams();
const { bunch, bunchReady } = useBunch();
const { userBunches, userBunchesReady } = useUserBunchList();

const slug = computed(() => {
  if (params.slug.value) return params.slug.value;
  if (userBunchesReady.value && userBunches.value.length > 0) return userBunches.value[0].id;
  return '';
});

const bunchName = computed(() => {
  if (params.slug.value && bunchReady.value && bunch.value.name.length > 0) return bunch.value.name;
  if (userBunchesReady.value && userBunches.value.length > 0) return userBunches.value[0].name;
  return '';
});

const hasSlug = computed(() => {
  return !!slug.value;
});

const bunchDetailsUrl = computed(() => urls.bunch.details(slug.value));
const cashgamesUrl = computed(() => urls.cashgame.index(slug.value));
const playersUrl = computed(() => urls.player.list(slug.value));
const eventsUrl = computed(() => urls.event.list(slug.value));
const locationsUrl = computed(() => urls.location.list(slug.value));
</script>
