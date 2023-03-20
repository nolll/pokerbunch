<template>
  <Layout :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <template v-slot:aside1>
          <Block>
            <CustomButton :url="addLocationUrl" type="action" text="Add location" />
          </Block>
        </template>

        <template v-slot:default>
          <Block>
            <PageHeading text="Locations" />
          </Block>

          <Block>
            <LocationList :bunch-id="params.slug.value" :locations="locations" />
          </Block>
        </template>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import LocationList from '@/components/LocationList/LocationList.vue';
import Block from '@/components/Common/Block.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import urls from '@/urls';
import useUsers from '@/composables/useUsers';
import { computed, onMounted } from 'vue';
import { useLocationsQuery } from '@/queries/locationQueries';
import useParams from '@/helpers/useParams';

const users = useUsers();
const params = useParams();
const locationsQuery = useLocationsQuery(params.slug.value);

const addLocationUrl = computed(() => {
  return urls.location.add(params.slug.value);
});

const locations = computed(() => {
  return locationsQuery.data.value ?? [];
});

const ready = computed(() => {
  return locationsQuery.isSuccess.value;
});

const init = () => {
  users.requireUser();
};

onMounted(() => {
  init();
});
</script>
