<template>
  <Layout :ready="ready">
    <PageSection>
      <Block>
        <PageHeading text="Create Bunch" />
      </Block>

      <template v-if="isSaving">
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
        <p v-if="hasError" class="validation-error">
          {{ errorMessage }}
        </p>
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
import Layout from '@/components/Layouts/Layout.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import CustomLink from '@/components/Common/CustomLink.vue';
import { AxiosError } from 'axios';
import { ApiError } from '@/models/ApiError';
import { ApiParamsAddBunch } from '@/models/ApiParamsAddBunch';
import TimezoneDropdown from '@/components/TimezoneDropdown.vue';
import CurrencyLayoutDropdown from '@/components/CurrencyLayoutDropdown.vue';
import useTimezones from '@/composables/useTimezones';
import { computed, onMounted, ref, watch } from 'vue';

const timezones = useTimezones();

const description = ref('');
const name = ref('');
const currencySymbol = ref('$');
const currencyLayout = ref('');
const timezone = ref('');
const errorMessage = ref('');
const isSaving = ref(false);
const savedSlug = ref('');

const hasError = computed(() => {
  return !!errorMessage.value;
});

const bunchUrl = computed(() => {
  return urls.bunch.details(savedSlug.value);
});

const save = async () => {
  errorMessage.value = '';

  try {
    const params: ApiParamsAddBunch = {
      name: name.value,
      description: description.value,
      currencySymbol: currencySymbol.value,
      currencyLayout: currencyLayout.value,
      timezone: timezone.value,
    };

    const response = await api.addBunch(params);
    savedSlug.value = response.data.id;
    isSaving.value = true;
  } catch (err) {
    const error = err as AxiosError<ApiError>;
    const message = error.response?.data.message || 'Unknown Error';
    errorMessage.value = message;
  }
};

const back = () => {
  history.back();
};

const ready = computed(() => {
  return timezones.timezonesReady.value;
});

const init = () => {
  timezones.loadTimezones();
};

onMounted(() => {
  init();
});
</script>
