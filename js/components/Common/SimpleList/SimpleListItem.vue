<template>
  <li class="simple-list__item">
    <slot></slot>
  </li>
</template>

<script setup lang="ts"></script>

<style lang="scss" scoped>
.simple-list__item {
  padding: 5px 0;
}

.simple-list__item + .simple-list__item {
  border-top: 1px solid #e7ede7;
}
</style>
