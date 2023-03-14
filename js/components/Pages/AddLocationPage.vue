<template>
  <Layout :ready="ready">
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
import { useRoute, useRouter } from 'vue-router';
import useUsers from '@/composables/useUsers';
import useBunches from '@/composables/useBunches';
import { useAddLocationMutation, locationsQueryKey } from '@/composables/locationQueries';
import { useQueryClient } from 'vue-query';

const router = useRouter();
const route = useRoute();
const users = useUsers();
const bunches = useBunches();
const queryClient = useQueryClient();

const onAddSuccess = () => {
  queryClient.invalidateQueries(locationsQueryKey(route.params.slug as string));
};

const { mutate: addLocation } = useAddLocationMutation(route.params.slug as string, onAddSuccess);

const locationName = ref('');

const init = () => {
  users.requireUser();
  bunches.loadBunch();
};

const add = () => {
  if (locationName.value.length > 0) {
    addLocation({ name: locationName.value });
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
  return bunches.bunchReady.value;
});

onMounted(() => {
  init();
});
</script>
