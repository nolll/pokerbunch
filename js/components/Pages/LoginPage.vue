<template>
  <Layout :require-user="false" :ready="true">
    <PageSection>
      <Block>
        <PageHeading text="Sign in" />
        <p>Please sign in using your username and password. <CustomLink :url="resetPasswordUrl">Forgot password?</CustomLink></p>
        <p>If you are a new user, please <CustomLink :url="registerUrl">register</CustomLink>!</p>
        <LoginForm :returnUrl="returnUrl" />
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import LoginForm from '@/components/LoginForm.vue';
import { Block, CustomLink, PageHeading, PageSection } from '@/components/Common';
import urls from '@/urls';
import { computed, onMounted, watch } from 'vue';
import { useRouter } from 'vue-router';
import { useCurrentUser } from '@/composables';
import querystring from '@/querystring';

const router = useRouter();

const { isSignedIn } = useCurrentUser('');

const resetPasswordUrl = computed(() => {
  let url = urls.user.resetPassword;
  const returnUrl = querystring.get('returnurl');
  if (Boolean(returnUrl)) url += '?returnurl=' + returnUrl;
  return url;
});

const registerUrl = computed(() => {
  let url = urls.user.add;
  const returnUrl = querystring.get('returnurl');
  if (Boolean(returnUrl)) url += '?returnurl=' + returnUrl;
  return url;
});

const returnUrl = computed(() => querystring.get('returnurl'));

const redirectIfSignedIn = () => {
  if (isSignedIn.value) {
    router.push(urls.home);
  }
};

onMounted(() => {
  redirectIfSignedIn();
});

watch(isSignedIn, redirectIfSignedIn);
</script>
