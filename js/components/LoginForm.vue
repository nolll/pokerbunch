<template>
  <div>
    <ErrorMessage :message="errorMessage" />
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
        <button @click="login" class="button button--action" :disabled="isLoggingIn">Sign in</button>
        <span class="login-form__logging-in" v-if="isLoggingIn">Signing in...</span>
      </div>
    </fieldset>
  </div>
</template>

<script setup lang="ts">
import querystring from '@/querystring';
import api from '@/api';
import auth from '@/auth';
import { ApiParamsLogin } from '@/models/ApiParamsLogin';
import { ref } from 'vue';
import ErrorMessage from '@/components/Common/ErrorMessage.vue';

const username = ref('');
const password = ref('');
const rememberMe = ref(false);
const errorMessage = ref<string | null>(null);
const isLoggingIn = ref(false);

const login = async () => {
  clearError();

  if (validateForm()) {
    var data: ApiParamsLogin = {
      username: username.value,
      password: password.value,
    };

    try {
      isLoggingIn.value = true;
      const response = /*mutate*/ await api.login(data);
      saveToken(response.data);
      redirect();
    } catch {
      isLoggingIn.value = false;
      showError('There was something wrong with your username or password. Please try again.');
    }
  } else {
    isLoggingIn.value = false;
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

<style lang="scss">
.login-form__logging-in {
  margin-left: 1rem;
}
</style>
