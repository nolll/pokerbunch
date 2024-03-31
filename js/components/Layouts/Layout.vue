<template>
  <div>
    <PageSection>
      <div class="page-header">
        <div class="logo"><CustomLink :url="homeUrl" :cssClasses="logoCssClasses">Poker Bunch</CustomLink></div>
        <div v-if="isTopNavEnabled">
          <slot name="top-nav"></slot>
        </div>
      </div>
    </PageSection>

    <div v-if="ready">
      <div class="main clearfix">
        <slot></slot>
      </div>

      <PageSection>
        <slot name="bottom-nav"><UserNavigation /></slot>
      </PageSection>
    </div>

    <div v-else>
      <LoadingSpinner />
    </div>
  </div>
</template>

<script setup lang="ts">
import UserNavigation from '@/components/Navigation/UserNavigation.vue';
import { CustomLink, LoadingSpinner, PageSection } from '@/components/Common';
import urls from '@/urls';
import { computed, useSlots, watch } from 'vue';
import { CssClasses } from '@/models/CssClasses';
import { useRoute } from 'vue-router';
import { useCurrentUser } from '@/composables';

const route = useRoute();
const props = defineProps<{
  ready: boolean;
  requireUser: boolean;
}>();

const { isSignedIn, currentUserReady } = useCurrentUser();

const slots = useSlots();

const isTopNavEnabled = computed(() => {
  return isSlotEnabled('top-nav');
});

const homeUrl = computed(() => {
  return urls.home;
});

const logoCssClasses = computed((): CssClasses => {
  return {
    'logo-link': true,
  };
});

const isSlotEnabled = (name: string) => {
  return !!slots[name];
};

const redirectIfSignedOut = () => {
  if (props.requireUser && currentUserReady.value && !isSignedIn.value)
    window.location.href = `${urls.auth.login}?returnurl=${route.fullPath}`;
};

watch(isSignedIn, redirectIfSignedOut);
watch(currentUserReady, redirectIfSignedOut);
watch(() => props.requireUser, redirectIfSignedOut);
</script>
