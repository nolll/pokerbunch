<template>
  <th class="table-list__column-header" :class="cssClasses">
    <span class="table-list__column-header__content" v-if="hasContent" v-on:click="onClick">
      <slot></slot>
    </span>
  </th>
</template>

<script setup lang="ts">
import { CssClasses } from '@/models/CssClasses';
import { computed, useSlots } from 'vue';

const props = defineProps<{
  sortName?: string;
  orderedBy?: string;
}>();

const emit = defineEmits(['sort']);

const slots = useSlots();

const hasContent = computed(() => !!slots.default);
const isSelected = computed(() => props.sortName === props.orderedBy);
const isSortable = computed(() => !!props.sortName);

const cssClasses = computed((): CssClasses => {
  return {
    'table-list__column-header--sortable': isSortable.value,
    'table-list__column-header--selected': isSortable.value && isSelected.value,
  };
});

const onClick = () => {
  if (isSortable.value) emit('sort', props.sortName);
};
</script>
