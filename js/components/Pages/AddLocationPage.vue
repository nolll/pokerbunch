<template>
  <Layout :require-user="true" :ready="true">
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
          <ErrorMessage :message="errorMessage" />
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
import { Layout } from '@/components/Layouts';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import { Block, CustomButton, ErrorMessage, PageHeading, PageSection } from '@/components/Common';
import urls from '@/urls';
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import useParams from '@/composables/useParams';
import api from '@/api';
import { locationListKey } from '@/queries/queryKeys';

const { slug } = useParams();
const router = useRouter();
const queryClient = useQueryClient();

const locationName = ref('');
const errorMessage = ref('');

const addMutation = useMutation({
  mutationFn: async () => {
    const response = await api.addLocation(slug.value, { name: locationName.value });
    return response.data;
  },
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: locationListKey(slug.value) });
    redirect();
  },
  onError: () => {
    errorMessage.value = 'Server error';
  },
});

const add = () => {
  if (locationName.value.length > 0) {
    addMutation.mutate();
    redirect();
  } else {
    errorMessage.value = "Name can't be empty";
  }
};

const cancel = () => {
  redirect();
};

const redirect = () => {
  router.push(urls.location.list(slug.value));
};
</script>
