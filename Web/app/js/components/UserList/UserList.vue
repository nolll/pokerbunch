<template>
  <SimpleList>
    <SimpleListItem v-for="user in userList" :key="user.userName">
      <UserListItem :userName="user.userName" />
    </SimpleListItem>
  </SimpleList>
</template>

<script setup lang="ts">
import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
import UserListItem from '@/components/UserList/UserListItem.vue';
import useUsers from '@/composables/useUsers';
import { computed } from 'vue';

const users = useUsers();

const userList = computed(() => {
  return users.users.value;
});
</script>
