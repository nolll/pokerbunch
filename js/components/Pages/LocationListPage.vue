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
            <LocationList :slug="slug" :locations="locations" />
          </Block>
        </template>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import LocationList from '@/components/LocationList/LocationList.vue';
import Block from '@/components/Common/Block.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import urls from '@/urls';
import { computed } from 'vue';
import useLocationList from '@/composables/useLocationList';
import useParams from '@/composables/useParams';

const { slug } = useParams();
const { locations, locationsReady } = useLocationList(slug.value);

const addLocationUrl = computed(() => {
  return urls.location.add(slug.value);
});

const ready = computed(() => {
  return locationsReady.value;
});
</script>
