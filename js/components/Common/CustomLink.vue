<template>
  <router-link :to="checkedUrl" :class="cssClasses" v-if="isInRouter">
    <slot></slot>
  </router-link>
  <a :href="checkedUrl" :class="cssClasses" v-else>
    <slot></slot>
  </a>
</template>

<script setup lang="ts">
import { CssClasses } from '@/models/CssClasses';
import { useRouter } from 'vue-router';
import { computed } from 'vue';

const router = useRouter();

const props = withDefaults(
  defineProps<{
    url?: string;
    cssClasses?: CssClasses;
  }>(),
  {
    url: '',
    cssClasses: () => {
      return {};
    },
  }
);

const isInRouter = computed(() => {
  if (!props.url) return false;
  let resolved = router.resolve(props.url);
  return resolved && resolved.name != '404';
});

const checkedUrl = computed(() => props.url || '#');
</script>
