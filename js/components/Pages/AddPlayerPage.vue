<template>
  <Layout :require-user="true" :ready="true">
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
          <ErrorMessage :message="errorMessage" />
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
import ErrorMessage from '@/components/Common/ErrorMessage.vue';
import urls from '@/urls';
import { useRouter } from 'vue-router';
import { ref } from 'vue';
import useParams from '@/composables/useParams';
import api from '@/api';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import { playerListKey } from '@/queries/queryKeys';

const { slug } = useParams();
const router = useRouter();
const queryClient = useQueryClient();

const playerName = ref('');
const errorMessage = ref('');

const addMutation = useMutation({
  mutationFn: async () => {
    await api.addPlayer(slug.value, { name: playerName.value });
  },
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: playerListKey(slug.value) });
    redirect();
  },
  onError: () => {
    errorMessage.value = 'Server error';
  },
});

const add = () => {
  if (playerName.value.length > 0) {
    addMutation.mutate();
  } else {
    errorMessage.value = "Name can't be empty";
  }
};

const cancel = () => redirect();

const redirect = () => {
  router.push(urls.player.list(slug.value));
};
</script>
