<template>
  <Layout :require-user="true" :ready="true">
    <PageSection>
      <Block>
        <PageHeading text="Create Bunch" />
      </Block>

      <template v-if="bunchAdded">
        <Block>
          <p>Your bunch has been created.</p>
          <p>
            <CustomLink :url="bunchUrl">Go to bunch!</CustomLink>
          </p>
        </Block>
      </template>
      <Block v-else>
        <p>
          <label class="label" for="name">Name</label>
          <input class="textfield" v-model="name" id="name" type="text" />
        </p>
        <p>
          <label class="label" for="description">Description</label>
          <input class="textfield" v-model="description" id="description" type="text" />
        </p>
        <p>
          <label class="label" for="currencySymbol">Currency Symbol</label>
          <input class="textfield" v-model="currencySymbol" id="currencySymbol" type="text" />
        </p>
        <p>
          <label class="label" for="currencyLayout">Currency Layout</label>
          <CurrencyLayoutDropdown v-model="currencyLayout" :symbol="currencySymbol" />
        </p>
        <p>
          <label class="label" for="timezone">Timezone</label>
          <TimezoneDropdown v-model="timezone" />
        </p>
        <ErrorMessage :message="errorMessage" />
        <p>
          <CustomButton v-on:click="save" type="action" text="Save" />
          <CustomButton v-on:click="back" text="Cancel" />
        </p>
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import urls from '@/urls';
import api from '@/api';
import { Layout } from '@/components/Layouts';
import { Block, CustomButton, CustomLink, ErrorMessage, PageHeading, PageSection } from '@/components/Common';
import { AxiosError } from 'axios';
import { ApiError } from '@/models/ApiError';
import { ApiParamsAddBunch } from '@/models/ApiParamsAddBunch';
import TimezoneDropdown from '@/components/TimezoneDropdown.vue';
import CurrencyLayoutDropdown from '@/components/CurrencyLayoutDropdown.vue';
import useTimezones from '@/composables/useTimezones';
import { computed, ref } from 'vue';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import { bunchListKey, userBunchListKey } from '@/queries/queryKeys';
import { BunchResponse } from '@/response/BunchResponse';

const timezones = useTimezones();
const defaultTimezone = timezones.getDefaultTimezone();
const queryClient = useQueryClient();

const description = ref('');
const name = ref('');
const currencySymbol = ref('$');
const currencyLayout = ref('');
const timezone = ref(defaultTimezone);
const errorMessage = ref('');
const bunchAdded = ref(false);
const savedSlug = ref('');

const hasError = computed(() => {
  return !!errorMessage.value;
});

const bunchUrl = computed(() => {
  return urls.bunch.details(savedSlug.value);
});

const save = async () => {
  addBunchMutation.mutate();
};

const addBunchMutation = useMutation({
  mutationFn: async () => {
    errorMessage.value = '';

    const params: ApiParamsAddBunch = {
      name: name.value,
      description: description.value,
      currencySymbol: currencySymbol.value,
      currencyLayout: currencyLayout.value,
      timezone: timezone.value,
    };

    const response = await api.addBunch(params);
    return response.data;
  },
  onSuccess: (response: BunchResponse) => {
    queryClient.invalidateQueries({ queryKey: bunchListKey() });
    queryClient.invalidateQueries({ queryKey: userBunchListKey(true) });
    savedSlug.value = response.id;
    bunchAdded.value = true;
  },
  onError: (err) => {
    const error = err as AxiosError<ApiError>;
    errorMessage.value = error.response?.data.message || 'Unknown Error';
  },
});

const back = () => {
  history.back();
};
</script>
