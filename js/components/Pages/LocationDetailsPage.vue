<template>
  <Layout :require-user="true" :ready="ready">
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
import { Layout } from '@/components/Layouts';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import { Block, PageHeading, PageSection } from '@/components/Common';
import { computed } from 'vue';
import { useRoute } from 'vue-router';
import { useParams, useLocationList } from '@/composables';

const { slug } = useParams();
const route = useRoute();
const { getLocation, locationsReady } = useLocationList(slug.value);

const name = computed(() => {
  if (location.value) return location.value.name;
  return '';
});

const location = computed(() => {
  return getLocation(locationId.value);
});

const locationId = computed(() => {
  return route.params.id as string;
});

const ready = computed(() => {
  return locationsReady.value;
});
</script>
