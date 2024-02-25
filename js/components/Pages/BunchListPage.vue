<template>
  <Layout :ready="ready">
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
import Layout from '@/components/Layouts/Layout.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import BunchList from '@/components/BunchList/BunchList.vue';
import useUsers from '@/composables/useUsers';
import { computed, onMounted, watch } from 'vue';
import useBunchList from '@/composables/useBunchList';

const users = useUsers();
const { bunches, bunchesReady } = useBunchList();

const ready = computed(() => {
  return users.userReady.value && bunchesReady.value;
});

const isAdmin = computed(() => {
  return users.isAdmin.value;
});

const init = () => {
  users.requireUser();
};

onMounted(() => {
  init();
});
</script>
