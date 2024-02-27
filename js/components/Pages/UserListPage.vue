<template>
  <Layout :require-user="true" :ready="ready">
    <PageSection>
      <Block>
        <PageHeading text="Users" />
      </Block>

      <Block v-if="isAdmin">
        <UserList />
      </Block>

      <Block v-else> Access denied </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import UserList from '@/components/UserList/UserList.vue';
import useUsers from '@/composables/useUsers';
import { computed, onMounted } from 'vue';

const users = useUsers();

const ready = computed(() => {
  return users.userReady.value && users.usersReady.value;
});

const isAdmin = computed(() => {
  return users.isAdmin.value;
});

const init = () => {
  users.requireUser();
  users.loadUsers();
};

onMounted(() => {
  init();
});
</script>
