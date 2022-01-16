<template>
  <Layout :ready="ready">
    <template slot="top-nav">
      <BunchNavigation />
    </template>

    <PageSection>
      <Block>
        <PageHeading text="This is Poker Bunch" />
      </Block>

      <div v-if="isSignedIn">
        <Block>
          <p>
            Poker Bunch helps you keep track of the results in your poker homegames. Please select one of your bunches, or
            <CustomLink :url="addBunchUrl">create a new bunch</CustomLink>.
          </p>
          <p>If you want to join an existing bunch, you will need an invitation from a bunch player.</p>
        </Block>
        <Block>
          <h2 class="module-heading">Api</h2>
          <p>
            The
            <CustomLink :url="apiDocsUrl">api</CustomLink>makes it possible to create your own apps that interact with Poker
            Bunch.
          </p>
        </Block>
      </div>

      <div v-else>
        <Block>
          <p>Poker Bunch helps you keep track of the results in your poker homegames.</p>
        </Block>
        <Block>
          <p>
            <CustomLink :url="loginUrl">Sign in</CustomLink>if you already have an account, or
            <CustomLink :url="registerUrl">register</CustomLink>to create a bunch and begin inviting players.
          </p>
        </Block>
      </div>

      <template slot="aside2">
        <Block>
          <UserBunchList />
        </Block>
        <Block v-if="isAdmin">
          <AdminNavigation />
        </Block>
      </template>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import AdminNavigation from '@/components/Navigation/AdminNavigation.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import urls from '@/urls';
import CustomLink from '@/components/Common/CustomLink.vue';
import UserBunchList from '@/components/UserBunchList/UserBunchList.vue';
import { computed, onMounted, watch } from 'vue';
import useUsers from '@/composables/useUsers';
import useBunches from '@/composables/useBunches';
import { useRoute } from 'vue-router';

const users = useUsers();
const bunches = useBunches();
const route = useRoute();

const isSignedIn = computed(() => {
  return users.isSignedIn.value;
});

const isAdmin = computed(() => {
  return users.isAdmin.value;
});

const loginUrl = computed(() => {
  return urls.auth.login;
});

const registerUrl = computed(() => {
  return urls.user.add;
});

const addBunchUrl = computed(() => {
  return urls.bunch.add;
});

const apiDocsUrl = computed(() => {
  return urls.api.docs;
});

const ready = computed(() => {
  return users.userReady.value && bunches.userBunchesReady.value;
});

const init = () => {
  users.loadCurrentUser();
  bunches.loadUserBunches();
};

onMounted(() => {
  init();
});

watch(route, () => {
  init();
});
</script>
