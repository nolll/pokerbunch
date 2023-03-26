<template>
  <Layout :ready="ready">
    <PageSection>
      <Block>
        <PageHeading text="Admin Tools" />
      </Block>

      <template v-if="canClearCache">
        <Block>
          <ClearCache />
        </Block>
      </template>

      <template v-if="canSendTestEmail">
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
import { computed, onMounted } from 'vue';
import auth from '@/auth';
import { useCurrentUserQuery } from '@/queries/userQueries';
import accessControl from '@/access-control';

const currentUserQuery = useCurrentUserQuery(auth.isLoggedIn());

const canClearCache = computed(() => accessControl.canClearCache(user.value.role));
const canSendTestEmail = computed(() => accessControl.canSendTestEmail(user.value.role));
const user = computed(() => currentUserQuery.data.value!);

const ready = computed(() => {
  return currentUserQuery.isSuccess.value;
});

onMounted(() => auth.requireUser());
</script>
