<template>
  <Layout :require-user="true" :ready="true">
    <PageSection v-if="isFormVisible">
      <Block>
        <PageHeading text="Join Bunch" />
      </Block>

      <Block>
        <p>Please enter your invitation code below to join the bunch</p>
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
          <CustomButton @click="join" type="action" text="Join" />
          <CustomButton @click="cancel" text="Cancel" />
        </div>
      </Block>
    </PageSection>
    <PageSection v-if="hasJoined">
      <Block>
        <PageHeading text="Success!" />
      </Block>

      <Block>
        <p>You have successfully joined the bunch. You need to sign out and and in again to access it.</p>
      </Block>
      <Block>
        <CustomButton @click="logOut" type="action" text="Sign out" />
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import { Block, CustomButton, PageHeading, PageSection } from '@/components/Common';
import urls from '@/urls';
import api from '@/api';
import { ApiError } from '@/models/ApiError';
import { AxiosError } from 'axios';
import { computed, onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
import { useParams } from '@/composables';
import { useMutation } from '@tanstack/vue-query';
import { MessageResponse } from '@/response/MessageResponse';
import auth from '@/auth';

const { slug, code } = useParams();
const router = useRouter();

const inputCode = ref('');
const errorMessage = ref<string>();
const hasJoined = ref(false);

const isFormVisible = computed(() => !hasJoined.value);

const join = () => {
  joinBunchMutation.mutate();
};

const logOut = () => {
  auth.logout();
  redirectToLogin();
};

const joinBunchMutation = useMutation({
  mutationFn: async (): Promise<MessageResponse> => {
    const data = {
      code: code.value.length > 0 ? code.value : inputCode.value,
    };

    const response = await api.joinBunch(slug.value, data);
    return response.data;
  },
  onSuccess: () => {
    hasJoined.value = true;
  },
  onError: (error: AxiosError<ApiError>) => {
    const message = error.response?.data.message || 'Unknown Error';
    errorMessage.value = message;
  },
});

const cancel = () => {
  router.push(urls.home);
};

const redirectToLogin = () => {
  window.location.href = urls.auth.login;
};

onMounted(async () => {
  if (code.value) join();
});
</script>
