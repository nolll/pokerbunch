<template>
  <nav class="user-nav" v-if="userReady">
    <h2>Account</h2>
    <ul v-if="isSignedIn">
      <li>
        <CustomLink :url="userDetailsUrl"
          ><span>Signed in as {{ displayName }}</span></CustomLink
        >
      </li>
      <li>
        <a href="#" @click.prevent="logOut"><span>Sign Out</span></a>
      </li>
    </ul>
    <ul v-else>
      <li>
        <CustomLink :url="loginUrl"><span>Sign in</span></CustomLink>
      </li>
      <li>
        <CustomLink :url="registerUrl"><span>Register</span></CustomLink>
      </li>
      <li>
        <CustomLink :url="resetPasswordUrl"><span>Reset password</span></CustomLink>
      </li>
    </ul>
  </nav>
</template>

<script setup lang="ts">
import CustomLink from '@/components/Common/CustomLink.vue';
import urls from '@/urls';
import auth from '@/auth';
import api from '@/api';
import useUsers from '@/composables/useUsers';
import { computed } from 'vue';

const users = useUsers();

const logOut = () => {
  auth.clearToken();
  redirectHome();
};

const userReady = computed(() => {
  return users.userReady.value;
});

const isSignedIn = computed(() => {
  return users.isSignedIn.value;
});

const displayName = computed(() => {
  return users.displayName.value;
});

const userDetailsUrl = computed(() => {
  return urls.user.details(users.userName.value);
});

const registerUrl = computed(() => {
  return urls.user.add;
});

const resetPasswordUrl = computed(() => {
  return urls.user.resetPassword;
});

const loginUrl = computed(() => {
  return urls.auth.login;
});

const redirectHome = () => {
  window.location.href = urls.home;
};
</script>
