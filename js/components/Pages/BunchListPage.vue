<template>
  <Layout :ready="ready">
    <PageSection>
      <Block>
        <PageHeading text="Bunches" />
      </Block>

      <Block v-if="canListBunches">
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
import { computed, onMounted } from 'vue';
import { useBunchesQuery } from '@/composables/bunchQueries';
import accessControl from '@/access-control';

const users = useUsers();
const bunchesQuery = useBunchesQuery();

const bunches = computed(() => bunchesQuery.data.value ?? []);

const ready = computed(() => {
  return users.userReady.value && bunchesQuery.isSuccess.value;
});

const canListBunches = computed(() => {
  return accessControl.canListBunches(users.role.value);
});

const init = () => {
  users.requireUser();
};

onMounted(() => {
  init();
});
</script>
