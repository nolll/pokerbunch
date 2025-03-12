<template>
  <Layout :require-user="true" :ready="true">
    <PageSection>
      <Block>
        <PageHeading text="Change Password" />
      </Block>

      <template v-if="passwordWasSaved">
        <Block>
          <p>Your password was changed</p>
        </Block>
        <Block>
          <CustomButton type="action" v-on:click="back" text="Back" />
        </Block>
      </template>
      <Block v-else>
        <p>
          <label class="label" for="oldPassword">Old Password</label>
          <input class="textfield" v-model="oldPassword" id="oldPassword" type="password" />
        </p>
        <p>
          <label class="label" for="newPassword">New Password</label>
          <input class="textfield" v-model="newPassword" id="newPassword" type="password" />
        </p>
        <p>
          <label class="label" for="repeat">Repeat New Password</label>
          <input class="textfield" v-model="repeat" id="repeat" type="password" />
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
import api from '@/api';
import { Layout } from '@/components/Layouts';
import { Block, CustomButton, PageHeading, PageSection } from '@/components/Common';
import { AxiosError } from 'axios';
import { ApiError } from '@/models/ApiError';
import { ApiParamsChangePassword } from '@/models/ApiParamsChangePassword';
import { computed, ref } from 'vue';
import { useMutation } from '@tanstack/vue-query';
import { MessageResponse } from '@/response/MessageResponse';

const oldPassword = ref('');
const newPassword = ref('');
const repeat = ref('');
const errorMessage = ref('');
const passwordWasSaved = ref(false);

const hasError = computed(() => !!errorMessage.value);

const save = async () => {
  errorMessage.value = '';

  if (repeat.value !== newPassword.value) {
    errorMessage.value = "Passwords doesn't match";
    return;
  }

  resetPasswordMutation.mutate();
};

const resetPasswordMutation = useMutation({
  mutationFn: async (): Promise<MessageResponse> => {
    const params: ApiParamsChangePassword = {
      oldPassword: oldPassword.value,
      newPassword: newPassword.value,
    };

    const response = await api.changePassword(params);
    return response.data;
  },
  onSuccess: () => {
    passwordWasSaved.value = true;
  },
  onError: (error: AxiosError<ApiError>) => {
    const message = error.response?.data.message || 'Unknown Error';
    errorMessage.value = message;
  },
});

const back = () => {
  history.back();
};
</script>
