<template>
  <Layout :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <PageSection>
      <template v-slot:default>
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
              <CustomLink :url="apiDocsUrl">api</CustomLink> makes it possible to create your own apps that interact with Poker
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
              <CustomLink :url="loginUrl">Sign in</CustomLink> if you already have an account, or
              <CustomLink :url="registerUrl">register</CustomLink> to create a bunch and begin inviting players.
            </p>
          </Block>
        </div>
      </template>

      <template v-slot:aside2>
        <Block>
          <UserBunchList />
        </Block>
        <Block v-if="canSeeAdminMenu">
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
import { useUserBunchesQuery } from '@/queries/bunchQueries';
import auth from '@/auth';
import { useCurrentUserQuery } from '@/queries/userQueries';
import accessControl from '@/access-control';

const users = useUsers();
const currentUserQuery = useCurrentUserQuery();
const userBunchesQuery = useUserBunchesQuery();

const isSignedIn = computed(() => {
  return auth.isLoggedIn();
});

const canSeeAdminMenu = computed(() => {
  return accessControl.canSeeAdminMenu(currentUserQuery.data.value?.role);
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
  return currentUserQuery.isSuccess.value && userBunchesQuery.isSuccess.value;
});
</script>
