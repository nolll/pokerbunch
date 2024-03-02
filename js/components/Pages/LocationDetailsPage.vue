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
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import useBunches from '@/composables/useBunches';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import useParams from '@/composables/useParams';
import useLocationList from '@/composables/useLocationList';

const params = useParams();
const route = useRoute();
const bunches = useBunches();
const { getLocation, locationsReady } = useLocationList(params.slug.value);

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
  return bunches.bunchReady.value && locationsReady.value;
});

const init = () => {
  bunches.loadBunch();
};

onMounted(() => {
  init();
});
</script>
