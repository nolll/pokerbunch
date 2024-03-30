<template>
  <SimpleList>
    <SimpleListItem v-for="user in users" :key="user.userName">
      <UserListItem :userName="user.userName" />
    </SimpleListItem>
  </SimpleList>
</template>

<script setup lang="ts">
import { SimpleList, SimpleListItem } from '@/components/Common/SimpleList';
import UserListItem from '@/components/UserList/UserListItem.vue';
import { User } from '@/models/User';

defineProps<{
  users: User[];
}>();
</script>
