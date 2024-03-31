<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <PageHeading text="Start Cashgame" />
        </Block>

        <Block>
          <div class="field">
            <label class="label" for="locationId">Location</label>
            <LocationDropdown :locations="locations" v-model="locationId" />
          </div>
          <ErrorMessage :message="errorMessage" />
          <div class="buttons">
            <CustomButton v-on:click="add" type="action" text="Start" />
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
import { ApiParamsAddCashgame } from '@/models/ApiParamsAddCashgame';
import urls from '@/urls';
import api from '@/api';
import { AxiosError } from 'axios';
import { ApiError } from '@/models/ApiError';
import LocationDropdown from '@/components/LocationDropdown.vue';
import { computed, ref } from 'vue';
import { useRouter } from 'vue-router';
import { useLocationList, useParams } from '@/composables';
import { useMutation } from '@tanstack/vue-query';
import { DetailedCashgameResponse } from '@/response/DetailedCashgameResponse';

const { slug } = useParams();
const router = useRouter();
const { locations, locationsReady } = useLocationList(slug.value);

const locationId = ref('');
const errorMessage = ref('');

const add = async () => {
  errorMessage.value = '';

  if (locationId.value === '') {
    errorMessage.value = 'Please select a location.';
    return;
  }

  addMutation.mutate();
};

const addMutation = useMutation({
  mutationFn: async () => {
    const params: ApiParamsAddCashgame = {
      locationId: locationId.value,
    };

    const response = await api.addCashgame(slug.value, params);
    return response.data;
  },
  onSuccess: (data: DetailedCashgameResponse) => {
    redirectToGame(data.id);
  },
  onError: (error: AxiosError<ApiError>) => {
    const message = error.response?.data.message || 'Unknown Error';
    errorMessage.value = message;
  },
});

const cancel = () => {
  history.back();
};

const redirectToGame = (id: string) => {
  router.push(urls.cashgame.details(slug.value, id));
};

const ready = computed(() => locationsReady.value);
</script>
