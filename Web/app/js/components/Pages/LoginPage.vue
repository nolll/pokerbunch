<template>
  <Layout :ready="ready">
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
import Layout from '@/components/Layouts/Layout.vue';
import LoginForm from '@/components/LoginForm.vue';
import CustomLink from '@/components/Common/CustomLink.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import urls from '@/urls';
import useUsers from '@/composables/useUsers';
import { computed, onMounted, watch } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const users = useUsers();

const resetPasswordUrl = computed(() => {
  return urls.user.resetPassword;
});

const registerUrl = computed(() => {
  return urls.user.add;
});

const ready = computed(() => {
  return users.userReady.value;
});

const redirectIfSignedIn = () => {
  if (ready.value && users.isSignedIn.value) {
    router.push(urls.home);
  }
};

const init = () => {
  users.requireUser();
};

onMounted(() => {
  init();
  redirectIfSignedIn();
});

watch(ready, () => {
  redirectIfSignedIn();
});
</script>
