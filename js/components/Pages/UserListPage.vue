<template>
  <Layout :require-user="true" :ready="ready">
    <PageSection>
      <Block>
        <PageHeading text="Users" />
      </Block>

      <Block v-if="isAdmin">
        <UserList :users="users" />
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
import { computed, onMounted } from 'vue';
import useUserList from '@/composables/useUserList';
import useCurrentUser from '@/composables/useCurrentUser';

const { users, usersReady } = useUserList();
const { isAdmin, currentUserReady } = useCurrentUser();

const ready = computed(() => {
  return currentUserReady.value, usersReady.value;
});
</script>
