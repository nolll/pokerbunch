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

const bunches = useBunches();

const slug = computed(() => {
  if (bunches.slug.value && bunches.slug.value.length > 0) return bunches.slug.value;
  if (bunches.userBunches.value.length > 0) return bunches.userBunches.value[0].id;
  return '';
});

const bunchName = computed(() => {
  if (bunches.bunchName.value && bunches.bunchName.value.length > 0) return bunches.bunchName.value;
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
