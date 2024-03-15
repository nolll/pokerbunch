<template>
  <Layout :require-user="true" :ready="ready">
    <PageSection>
      <Block>
        <PageHeading text="Join Bunch" />
      </Block>

      <Block>
        <p>Please enter your invitation code below to join the bunch {{ bunchName }}</p>
      </Block>
      <Block v-if="errorMessage">
        <p class="validation-error">
          {{ errorMessage }}
        </p>
      </Block>
      <Block>
        <div class="field">
          <label class="label" for="invitationCode">Invitation Code</label>
          <input class="longfield" v-model="inputCode" id="invitationCode" type="text" />
        </div>
        <div class="buttons">
          <CustomButton @click="joinClicked" type="action" text="Join" />
          <CustomButton @click="cancel" text="Cancel" />
        </div>
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import Block from '@/components/Common/Block.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import urls from '@/urls';
import api from '@/api';
import { ApiError } from '@/models/ApiError';
import { AxiosError } from 'axios';
import { computed, ref, watch } from 'vue';
import { useRouter } from 'vue-router';
import useParams from '@/composables/useParams';
import useBunch from '@/composables/useBunch';

const { slug, code } = useParams();
const router = useRouter();
const { bunch, bunchReady } = useBunch(slug.value);

const inputCode = ref('');
const errorMessage = ref<string>();

const bunchName = computed(() => {
  return bunch.value.name;
});

const ready = computed(() => {
  return bunchReady.value;
});

const joinClicked = () => {
  join(slug.value, inputCode.value);
};

const join = async (bunchId: string, code: string) => {
  if (code.length > 0) {
    try {
      /*mutate*/ await api.joinBunch(bunchId, { code });
      router.push(urls.bunch.details(slug.value));
    } catch (err) {
      const error = err as AxiosError<ApiError>;
      errorMessage.value = error.response?.data.message || 'Unknown Error';
    }
  }
};

const cancel = () => {
  router.push(urls.home);
};

watch(ready, () => {
  if (ready.value && code.value) join(slug.value, code.value);
});
</script>
