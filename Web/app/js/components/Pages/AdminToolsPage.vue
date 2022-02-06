<template>
  <Layout :ready="ready">
    <PageSection>
      <Block>
        <PageHeading text="Admin Tools" />
      </Block>

      <template v-if="isAdmin">
        <Block>
          <ClearCache />
        </Block>

        <Block>
          <SendEmail />
        </Block>
      </template>

      <Block v-else>
        <p>Access denied</p>
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import SendEmail from '@/components/Admin/SendEmail.vue';
import ClearCache from '@/components/Admin/ClearCache.vue';
import useUsers from '@/composables/useUsers';
import { computed, onMounted, watch } from 'vue';

const users = useUsers();

const isAdmin = computed(() => {
  return users.isAdmin.value;
});

const ready = computed(() => {
  return users.userReady.value;
});

const init = () => {
  users.loadCurrentUser();
};

onMounted(() => {
  init();
});
</script>
