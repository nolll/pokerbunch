<template>
  <Layout :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <PageHeading :text="name" />
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import useUsers from '@/composables/useUsers';
import { computed, onMounted } from 'vue';
import { useLocationQuery } from '@/queries/locationQueries';
import useParams from '@/helpers/useParams';

const users = useUsers();
const params = useParams();
const locationQuery = useLocationQuery(params.id.value);

const name = computed(() => {
  if (location.value) return location.value.name;
  return '';
});

const location = computed(() => {
  return locationQuery.data.value;
});

const ready = computed(() => {
  return locationQuery.isSuccess.value;
});

const init = () => {
  users.requireUser();
};

onMounted(() => {
  init();
});
</script>
