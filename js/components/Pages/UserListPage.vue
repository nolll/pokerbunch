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
import { Layout } from '@/components/Layouts';
import { Block, PageHeading, PageSection } from '@/components/Common';
import UserList from '@/components/UserList/UserList.vue';
import { computed } from 'vue';
import { useUserList, useCurrentUser } from '@/composables';

const { users, usersReady } = useUserList();
const { isAdmin } = useCurrentUser('');

const ready = computed(() => usersReady.value);
</script>
