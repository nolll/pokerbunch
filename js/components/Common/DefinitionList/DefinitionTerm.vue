<template>
  <dt class="definition-list-key">
    <slot></slot>
  </dt>
</template>

<script setup lang="ts"></script>

<style lang="scss" scoped>
@import 'styles/css/mixins';

.definition-list-key {
  @include fontsize(12);
  text-transform: uppercase;
}
</style>
