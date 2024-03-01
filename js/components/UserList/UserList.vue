<template>
  <SimpleList>
    <SimpleListItem v-for="user in users" :key="user.userName">
      <UserListItem :userName="user.userName" />
    </SimpleListItem>
  </SimpleList>
</template>

<script setup lang="ts">
import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
import UserListItem from '@/components/UserList/UserListItem.vue';
import { User } from '@/models/User';

defineProps<{
  users: User[];
}>();
</script>
