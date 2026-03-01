<template>
  <ul class="simple-list">
    <slot></slot>
  </ul>
</template>

<script setup lang="ts"></script>

<style lang="scss" scoped>
.simple-list {
  padding: 0;
  margin: 0;
  list-style-type: none;
}
</style>
