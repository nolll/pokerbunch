<template>
  <nav class="user-nav">
    <h2>Account</h2>
    <ul v-if="isSignedIn2">
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
import { CustomLink } from '@/components/Common';
import urls from '@/urls';
import auth from '@/auth';
import { computed } from 'vue';
import { useCurrentUser } from '@/composables';

const { isSignedIn2, currentUser2 } = useCurrentUser();

const logOut = () => {
  auth.clearToken();
  redirectHome();
};

const displayName = computed(() => currentUser2.value?.userDisplayName);
const userDetailsUrl = computed(() => (isSignedIn2.value ? urls.user.details(currentUser2.value.userName) : ''));
const registerUrl = computed(() => urls.user.add);
const resetPasswordUrl = computed(() => urls.user.resetPassword);
const loginUrl = computed(() => urls.auth.login);

const redirectHome = () => {
  window.location.href = urls.home;
};
</script>
