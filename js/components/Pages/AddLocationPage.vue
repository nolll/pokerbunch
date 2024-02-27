<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <PageHeading text="Add Location" />
        </Block>

        <Block>
          <div class="field">
            <label class="label" for="location-name">Name</label>
            <input class="textfield" v-model="locationName" id="location-name" type="text" />
          </div>
          <div class="buttons">
            <CustomButton v-on:click="add" type="action" text="Add" />
            <CustomButton v-on:click="cancel" text="Cancel" />
          </div>
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import Block from '@/components/Common/Block.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import urls from '@/urls';
import { computed, onMounted, ref, watch } from 'vue';
import { useRouter } from 'vue-router';
import useUsers from '@/composables/useUsers';
import useBunches from '@/composables/useBunches';
import useLocations from '@/composables/useLocations';

const router = useRouter();
const users = useUsers();
const bunches = useBunches();
const locations = useLocations();

const locationName = ref('');

const init = () => {
  users.requireUser();
  bunches.loadBunch();
  locations.loadLocations();
};

const add = () => {
  if (locationName.value.length > 0) {
    locations.addLocation(locationName.value);
    redirect();
  }
};

const cancel = () => {
  redirect();
};

const redirect = () => {
  router.push(urls.location.list(bunches.slug.value));
};

const ready = computed(() => {
  return bunches.bunchReady.value && locations.locationsReady.value;
});

onMounted(() => {
  init();
});
</script>
