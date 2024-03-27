<template>
  <Layout :require-user="false" :ready="true">
    <PageSection>
      <Block>
        <PageHeading text="Reset Password" />
      </Block>

      <template v-if="emailWasSent">
        <Block>
          <p>An email was sent.</p>
        </Block>
        <Block>
          <CustomButton type="action" :url="loginUrl" text="Sign in" />
        </Block>
      </template>
      <Block v-else>
        <p>Enter your email address to reset your password.</p>
        <p>
          <label class="label" for="email">Email</label>
          <input class="textfield" v-model="email" id="email" type="text" />
        </p>
        <p v-if="hasError" class="validation-error">
          {{ errorMessage }}
        </p>
        <p>
          <CustomButton v-on:click="send" type="action" text="Send" />
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
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import { AxiosError } from 'axios';
import { ApiError } from '@/models/ApiError';
import { ApiParamsResetPassword } from '@/models/ApiParamsResetPassword';
import { computed, onMounted, ref } from 'vue';
import { useMutation } from '@tanstack/vue-query';
import { MessageResponse } from '@/response/MessageResponse';

const email = ref('');
const errorMessage = ref('');
const emailWasSent = ref(false);

const hasError = computed(() => {
  return !!errorMessage.value;
});

const loginUrl = computed(() => {
  return urls.auth.login;
});

const send = async () => {
  errorMessage.value = '';

  resetPasswordMutation.mutate();
};

const resetPasswordMutation = useMutation({
  mutationFn: async (): Promise<MessageResponse> => {
    const params: ApiParamsResetPassword = {
      email: email.value,
    };

    const response = await api.resetPassword(params);
    return response.data;
  },
  onSuccess: () => {
    emailWasSent.value = true;
  },
  onError: (error: AxiosError<ApiError>) => {
    const message = error.response?.data.message || 'Unknown Error';
    errorMessage.value = message;
  },
});

const back = () => {
  history.back();
};

const init = () => {};

onMounted(() => {
  init();
});
</script>
