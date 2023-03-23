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
import { computed, onMounted } from 'vue';
import { useBunchesQuery } from '@/queries/bunchQueries';
import accessControl from '@/access-control';
import auth from '@/auth';
import { useCurrentUserQuery } from '@/queries/userQueries';

const currentUserQuery = useCurrentUserQuery(auth.isLoggedIn());
const bunchesQuery = useBunchesQuery();

const bunches = computed(() => bunchesQuery.data.value ?? []);
const user = computed(() => currentUserQuery.data.value!);

const ready = computed(() => {
  return currentUserQuery.isSuccess.value && bunchesQuery.isSuccess.value;
});

const canListBunches = computed(() => {
  return accessControl.canListBunches(user.value.role);
});

onMounted(() => auth.requireUser());
</script>
