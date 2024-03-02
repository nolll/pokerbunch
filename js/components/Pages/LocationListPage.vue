<template>
  <Layout :require-user="true" :ready="ready">
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
            <LocationList :locations="locations" />
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
import useBunches from '@/composables/useBunches';
import { computed, onMounted } from 'vue';
import useLocationList from '@/composables/useLocationList';
import useParams from '@/composables/useParams';

const params = useParams();
const bunches = useBunches();
const { locations, locationsReady } = useLocationList(params.slug.value);

const addLocationUrl = computed(() => {
  return urls.location.add(bunches.slug.value);
});

const ready = computed(() => {
  return bunches.bunchReady.value && locationsReady.value;
});

const init = () => {
  bunches.loadBunch();
};

onMounted(() => {
  init();
});
</script>
