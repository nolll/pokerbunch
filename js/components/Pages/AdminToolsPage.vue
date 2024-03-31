<template>
  <Layout :require-user="true" :ready="ready">
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
import { Layout } from '@/components/Layouts';
import { Block, PageHeading, PageSection } from '@/components/Common';
import SendEmail from '@/components/Admin/SendEmail.vue';
import ClearCache from '@/components/Admin/ClearCache.vue';
import { computed } from 'vue';
import { useCurrentUser } from '@/composables';

const { isAdmin, currentUserReady } = useCurrentUser();

const ready = computed(() => {
  return currentUserReady.value;
});
</script>
