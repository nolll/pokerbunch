<template>
  <tr class="table-list__row">
    <slot></slot>
  </tr>
</template>

<script setup lang="ts"></script>

<style lang="scss" scoped>
.table-list__row + .table-list__row {
  border-top: 1px solid #e7ede7;
}
</style>
