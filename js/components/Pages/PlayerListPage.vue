<template>
  <Layout :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <template v-slot:aside1>
          <Block>
            <CustomButton :url="addPlayerUrl" type="action" text="Add player" />
          </Block>
        </template>

        <template v-slot:default>
          <Block>
            <PageHeading text="Players" />
          </Block>

          <Block>
            <PlayerList :bunchId="slug" />
          </Block>
        </template>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import PlayerList from '@/components/PlayerList/PlayerList.vue';
import Block from '@/components/Common/Block.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import urls from '@/urls';
import useUsers from '@/composables/useUsers';
import useBunches from '@/composables/useBunches';
import usePlayers from '@/composables/usePlayers';
import { computed, onMounted } from 'vue';

const users = useUsers();
const bunches = useBunches();
const players = usePlayers();

const addPlayerUrl = computed(() => {
  return urls.player.add(slug.value);
});

const slug = computed(() => {
  return bunches.slug.value;
});

const ready = computed(() => {
  return bunches.bunchReady.value && players.playersReady.value;
});

const init = () => {
  users.requireUser();
  bunches.loadBunch();
  players.loadPlayers();
};

onMounted(() => {
  init();
});
</script>
