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
import PageSection from '@/components/Common/PageSection.vue';
import LoadingSpinner from '@/components/Common/LoadingSpinner.vue';
import CustomLink from '@/components/Common/CustomLink.vue';
import urls from '@/urls';
import { computed, useSlots } from 'vue';
import { CssClasses } from '@/models/CssClasses';

const slots = useSlots();
defineProps<{
  ready: boolean;
}>();

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
  return !!slots[name]; // todo: this probably won't work in vue 3
};
</script>
