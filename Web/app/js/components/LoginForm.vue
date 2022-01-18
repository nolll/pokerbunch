<template>
  <div>
    <div class="errors" v-if="isErrorVisible">
      <p class="validation-error">
        {{ errorMessage }}
      </p>
    </div>
    <fieldset>
      <p>
        <label class="label" for="username">Email or User Name</label>
        <input type="text" id="username" v-model="username" class="textfield" />
      </p>
      <p>
        <label class="label" for="password">Password</label>
        <input type="password" id="password" v-model="password" class="textfield" />
      </p>
      <p class="checkbox-layout">
        <label class="checkbox-label" for="rememberme">Keep me signed in</label>
        <input type="checkbox" v-model="rememberMe" id="rememberme" />
      </p>
      <div class="buttons">
        <button @click="login" class="button button--action">Sign in</button>
      </div>
    </fieldset>
  </div>
</template>

<script setup lang="ts">
import querystring from '@/querystring';
import api from '@/api';
import auth from '@/auth';
import { ApiParamsGetToken } from '@/models/ApiParamsGetToken';
import { computed, ref } from 'vue';

const username = ref('');
const password = ref('');
const rememberMe = ref(false);
const errorMessage = ref<string | null>(null);

const isErrorVisible = computed(() => {
  return errorMessage.value !== null;
});

const login = async () => {
  clearError();

  if (validateForm()) {
    var data: ApiParamsGetToken = {
      username: username.value,
      password: password.value,
    };

    try {
      const response = await api.getToken(data);
      saveToken(response.data);
      redirect();
    } catch {
      showError('There was something wrong with your username or password. Please try again.');
    }
  } else {
    showError('Please enter your username (or email) and password');
  }
};

const validateForm = () => {
  clearError();
  if (username.value === '' || password.value === '') return false;
  return true;
};

const clearError = () => {
  errorMessage.value = null;
};

const showError = (message: string) => {
  errorMessage.value = message;
};

const saveToken = (token: string) => {
  auth.setToken(token, rememberMe.value);
};

const redirect = () => {
  const returnUrl = querystring.get('returnurl');
  const redirectUrl = returnUrl || '/';
  window.location.href = redirectUrl;
};
</script>
