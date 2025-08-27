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
import { Block, PageHeading, PageSection } from '@/components/Common';
import BunchList from '@/components/BunchList/BunchList.vue';
import { computed } from 'vue';
import { useBunchList, useCurrentUser } from '@/composables';

const { bunches, bunchesReady } = useBunchList();
const { isAdmin } = useCurrentUser();

const ready = computed(() => bunchesReady.value);
</script>
