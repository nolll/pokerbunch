﻿<template>
  <Layout :require-user="false" :ready="ready">
    <PageSection>
      <Block>
        <PageHeading text="Sign in" />
        <p>Please sign in using your username and password. <CustomLink :url="resetPasswordUrl">Forgot password?</CustomLink></p>
        <p>If you are a new user, please <CustomLink :url="registerUrl">register</CustomLink>!</p>
        <LoginForm />
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import LoginForm from '@/components/LoginForm.vue';
import CustomLink from '@/components/Common/CustomLink.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import urls from '@/urls';
import { computed, onMounted, watch } from 'vue';
import { useRouter } from 'vue-router';
import useCurrentUser from '@/composables/useCurrentUser';

const router = useRouter();

const { isSignedIn, currentUserReady } = useCurrentUser();

const resetPasswordUrl = computed(() => {
  return urls.user.resetPassword;
});

const registerUrl = computed(() => {
  return urls.user.add;
});

const redirectIfSignedIn = () => {
  if (isSignedIn.value) {
    router.push(urls.home);
  }
};

onMounted(() => {
  redirectIfSignedIn();
});

watch(isSignedIn, redirectIfSignedIn);
watch(currentUserReady, redirectIfSignedIn);

const ready = computed(() => {
  return currentUserReady.value;
});
</script>
