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
import { Block, CustomButton, PageHeading, PageSection } from '@/components/Common';
import urls from '@/urls';
import { computed } from 'vue';
import { useLocationList, useParams } from '@/composables';

const { slug } = useParams();
const { locations, locationsReady } = useLocationList(slug.value);

const addLocationUrl = computed(() => urls.location.add(slug.value));
const ready = computed(() => locationsReady.value);
</script>
