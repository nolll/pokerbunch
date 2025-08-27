<template>
  <Layout :require-user="false" :ready="ready">
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
          <UserBunchList :bunches="userBunches" />
        </Block>
        <Block v-if="isAdmin">
          <AdminNavigation />
        </Block>
      </template>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import AdminNavigation from '@/components/Navigation/AdminNavigation.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import urls from '@/urls';
import { Block, CustomLink, PageHeading, PageSection } from '@/components/Common';
import UserBunchList from '@/components/UserBunchList/UserBunchList.vue';
import { computed } from 'vue';
import { useUserBunchList, useCurrentUser } from '@/composables';

const { isSignedIn, isAdmin } = useCurrentUser();
const { userBunchesReady, userBunches } = useUserBunchList(isSignedIn.value);

const loginUrl = computed(() => urls.auth.login);
const registerUrl = computed(() => urls.user.add);
const addBunchUrl = computed(() => urls.bunch.add);
const apiDocsUrl = computed(() => urls.api.docs);

const ready = computed(() => userBunchesReady.value);
</script>
