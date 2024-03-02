<template>
  <Layout :require-user="true" :ready="ready">
    <PageSection>
      <Block>
        <PageHeading text="Join Bunch" />
      </Block>

      <Block>
        <p>Please enter your invitation code below to join the bunch {{ bunchName }}</p>
      </Block>
      <Block v-if="errorMessage">
        <p class="validation-error">
          {{ errorMessage }}
        </p>
      </Block>
      <Block>
        <div class="field">
          <label class="label" for="invitationCode">Invitation Code</label>
          <input class="longfield" v-model="code" id="invitationCode" type="text" />
        </div>
        <div class="buttons">
          <CustomButton @click="joinClicked" type="action" text="Join" />
          <CustomButton @click="cancel" text="Cancel" />
        </div>
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import Block from '@/components/Common/Block.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import urls from '@/urls';
import api from '@/api';
import { ApiError } from '@/models/ApiError';
import { AxiosError } from 'axios';
import useBunches from '@/composables/useBunches';
import usePlayers from '@/composables/usePlayers';
import { computed, onMounted, ref, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();
const bunches = useBunches();
const players = usePlayers();

const code = ref('');
const errorMessage = ref<string>();

const routeCode = computed((): string | undefined => {
  return route.params.code as string;
});

const bunchName = computed(() => {
  return bunches.bunchName.value;
});

const ready = computed(() => {
  return bunches.bunchReady.value;
});

const joinClicked = () => {
  join(bunches.slug.value, code.value);
};

const join = async (bunchId: string, code: string) => {
  if (code.length > 0) {
    try {
      await api.joinBunch(bunchId, { code });
      players.loadPlayers();
      router.push(urls.bunch.details(bunches.slug.value));
    } catch (err) {
      const error = err as AxiosError<ApiError>;
      errorMessage.value = error.response?.data.message || 'Unknown Error';
    }
  }
};

const cancel = () => {
  router.push(urls.home);
};

const init = () => {
  bunches.loadBunch();
};

watch(ready, () => {
  if (ready.value && routeCode.value) join(bunches.slug.value, routeCode.value);
});

onMounted(() => {
  init();
});
</script>
