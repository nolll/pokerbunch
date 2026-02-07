<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <template v-slot:aside1>
          <Block v-if="isManager">
            <CustomButton :url="addPlayerUrl" type="action" text="Add player" />
          </Block>
          <Block v-if="isManager">
            <CustomButton :url="joinRequestsUrl" type="action" text="Join requests" />
          </Block>
        </template>

        <template v-slot:default>
          <Block>
            <PageHeading text="Players" />
          </Block>

          <Block>
            <PlayerList :bunchId="slug" :players="players" />
          </Block>
        </template>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import PlayerList from '@/components/PlayerList/PlayerList.vue';
import { Block, CustomButton, PageHeading, PageSection } from '@/components/Common';
import urls from '@/urls';
import { computed } from 'vue';
import { useCurrentUser, useParams, usePlayerList } from '@/composables';

const { slug } = useParams();
const { isManager } = useCurrentUser(slug.value);
const { players, playersReady } = usePlayerList(slug.value);

const addPlayerUrl = computed(() => urls.player.add(slug.value));
const joinRequestsUrl = computed(() => urls.joinRequest.list(slug.value));
const ready = computed(() => playersReady.value);
</script>
