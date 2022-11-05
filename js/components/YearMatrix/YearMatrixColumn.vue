<template>
  <TableListColumnHeader>
    {{ year }}
  </TableListColumnHeader>
</template>

<script setup lang="ts">
import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';

defineProps<{
  year: number;
}>();
</script>
