<template>
  <CustomLink :url="url" :cssClasses="cssClasses" v-if="hasUrl">{{ text }}</CustomLink>
  <button v-on:click="click" :class="cssClasses" v-else>{{ text }}</button>
</template>

<script setup lang="ts">
import CustomLink from '@/components/Common/CustomLink.vue';
import { ButtonType } from '@/models/ButtonType';
import { CssClasses } from '@/models/CssClasses';
import { computed } from 'vue';

const props = defineProps<{
  type: ButtonType;
  text: string;
  url?: string;
}>();

const emit = defineEmits(['click']);

const hasUrl = computed((): boolean => {
  return !!props.url;
});

const cssClasses = computed((): CssClasses => {
  return {
    button: true,
    'button--action': props.type === ButtonType.Action,
  };
});

const click = () => {
  emit('click');
};
</script>
