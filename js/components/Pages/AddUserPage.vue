<template>
  <Layout :require-user="false" :ready="true">
    <PageSection>
      <Block>
        <PageHeading text="Register" />
      </Block>

      <template v-if="userAdded">
        <Block>
          <p>Welcome to Poker Bunch!</p>
          <p><CustomLink :url="loginUrl">Sign in here!</CustomLink> GL!</p>
        </Block>
      </template>
      <Block v-else>
        <p>
          <label class="label" for="userName">Login Name</label>
          <input class="textfield" v-model="userName" id="userName" type="text" />
        </p>
        <p>
          <label class="label" for="displayName">Display Name</label>
          <input class="textfield" v-model="displayName" id="displayName" type="text" />
        </p>
        <p>
          <label class="label" for="email">Email</label>
          <input class="textfield" v-model="email" id="email" type="email" />
        </p>
        <p>
          <label class="label" for="password">Password</label>
          <input class="textfield" v-model="password" id="password" type="password" />
        </p>
        <p>
          <label class="label" for="repeatPassword">Repeat Password</label>
          <input class="textfield" v-model="repeatPassword" id="repeatPassword" type="password" />
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
import { ApiParamsAddUser } from '@/models/ApiParamsAddUser';
import { computed, ref } from 'vue';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import { userListKey } from '@/queries/queryKeys';

const queryClient = useQueryClient();

const userName = ref('');
const displayName = ref('');
const email = ref('');
const password = ref('');
const repeatPassword = ref('');
const errorMessage = ref('');
const userAdded = ref(false);

const hasError = computed(() => {
  return !!errorMessage.value;
});

const loginUrl = computed(() => {
  return urls.auth.login;
});

const save = async () => {
  if (repeatPassword.value !== password.value) {
    errorMessage.value = "Passwords doesn't match";
    return;
  }

  addUserMutation.mutate();
};

const addUserMutation = useMutation({
  mutationFn: async () => {
    errorMessage.value = '';

    const params: ApiParamsAddUser = {
      userName: userName.value,
      displayName: displayName.value,
      email: email.value,
      password: password.value,
    };

    const response = await api.addUser(params);
    return response.data;
  },
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: userListKey() });
    userAdded.value = true;
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
