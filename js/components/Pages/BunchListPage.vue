<template>
  <Layout :require-user="true" :ready="ready">
    <PageSection>
      <Block>
        <PageHeading text="Bunches" />
      </Block>

      <Block v-if="isAdmin">
        <BunchList :bunches="bunches" />
      </Block>

      <Block v-else> Access denied </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import BunchList from '@/components/BunchList/BunchList.vue';
import { computed } from 'vue';
import useBunchList from '@/composables/useBunchList';
import useCurrentUser from '@/composables/useCurrentUser';

const { bunches, bunchesReady } = useBunchList();
const { isAdmin, currentUserReady } = useCurrentUser();

const ready = computed(() => {
  return currentUserReady.value && bunchesReady.value;
});
</script>
