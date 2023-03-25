<template>
  <Layout :ready="true">
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
            <input class="textfield" v-model="playerName" id="player-name" type="text" />
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
import { useRouter } from 'vue-router';
import { computed, onMounted, ref } from 'vue';
import auth from '@/auth';
import useParams from '@/helpers/useParams';
import { playersQueryKey } from '@/queries/playerQueries';
import api from '@/api';
import { useQueryClient } from 'vue-query';

const params = useParams();
const router = useRouter();
const queryClient = useQueryClient();
const slug = computed(() => params.slug.value);

const playerName = ref('');

const add = async () => {
  if (playerName.value.length > 0) {
    await api.addPlayer(slug.value, { name: playerName.value });
    queryClient.invalidateQueries(playersQueryKey(slug.value));
    redirect();
  }
};

const cancel = () => {
  redirect();
};

const redirect = () => {
  router.push(urls.player.list(slug.value));
};

onMounted(() => auth.requireUser());
</script>
