<template>
  <Layout :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <PageHeading text="Add Player" />
        </Block>

        <Block>
          <div class="field">
            <label class="label" for="player-name">Name</label>
            <input class="textfield" v-model="playerName" ref="playerName" id="player-name" type="text" />
          </div>
          <div class="buttons">
            <CustomButton v-on:click="add" type="action" text="Add" />
            <CustomButton v-on:click="cancel" text="Cancel" />
          </div>
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import Block from '@/components/Common/Block.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import urls from '@/urls';
import useBunches from '@/composables/useBunches';
import useUsers from '@/composables/useUsers';
import usePlayers from '@/composables/usePlayers';
import { useRouter } from 'vue-router';
import { computed, onMounted, ref, watch } from 'vue';

const router = useRouter();
const users = useUsers();
const bunches = useBunches();
const players = usePlayers();

const playerName = ref('');

const init = () => {
  users.requireUser();
  bunches.loadBunch();
  players.loadPlayers();
};

const add = () => {
  if (playerName.value.length > 0) {
    players.addPlayer(playerName.value);
    redirect();
  }
};

const cancel = () => {
  redirect();
};

const redirect = () => {
  router.push(urls.player.list(bunches.slug.value));
};

const ready = computed(() => {
  return bunches.bunchReady.value && players.playersReady.value;
});

onMounted(() => {
  init();
});
</script>
