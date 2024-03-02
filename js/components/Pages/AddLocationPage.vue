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
import useBunches from '@/composables/useBunches';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import useParams from '@/composables/useParams';
import api from '@/api';
import { locationListKey } from '@/queries/queryKeys';

const params = useParams();
const router = useRouter();
const bunches = useBunches();
const queryClient = useQueryClient();

const locationName = ref('');

const init = () => {
  bunches.loadBunch();
};

const addLocation = async () => {
  await api.addLocation(params.slug.value, { name: locationName.value });
};

const addMutation = useMutation({
  mutationFn: addLocation,
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: locationListKey(params.slug.value) });
    redirect();
  },
});

const add = () => {
  if (locationName.value.length > 0) {
    addMutation.mutate();
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
