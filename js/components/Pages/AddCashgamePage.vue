<template>
  <Layout :ready="ready">
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
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import Block from '@/components/Common/Block.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import { ApiParamsAddCashgame } from '@/models/ApiParamsAddCashgame';
import urls from '@/urls';
import api from '@/api';
import { AxiosError } from 'axios';
import { ApiError } from '@/models/ApiError';
import LocationDropdown from '@/components/LocationDropdown.vue';
import { computed, onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
import { useLocationsQuery } from '@/queries/locationQueries';
import useParams from '@/helpers/useParams';
import auth from '@/auth';

const router = useRouter();
const params = useParams();
const locationsQuery = useLocationsQuery(params.slug.value);

const locationId = ref('');
const errorMessage = ref('');

const locations = computed(() => {
  return locationsQuery.data.value ?? [];
});

const add = async () => {
  errorMessage.value = '';

  try {
    const addCashgameParams: ApiParamsAddCashgame = {
      locationId: locationId.value,
    };

    const response = await api.addCashgame(params.slug.value, addCashgameParams);
    redirectToGame(response.data.id);
  } catch (err) {
    const error = err as AxiosError<ApiError>;
    const message = error.response?.data.message || 'Unknown Error';
    errorMessage.value = message;
  }
};

const cancel = () => {
  window.history.back();
};

const redirectToGame = (id: string) => {
  router.push(urls.cashgame.details(params.slug.value, id));
};

const ready = computed(() => {
  return locationsQuery.isSuccess.value;
});

onMounted(() => auth.requireUser());
</script>
