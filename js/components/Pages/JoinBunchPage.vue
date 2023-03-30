<template>
  <Layout :ready="ready">
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
import { computed, onMounted, ref, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import useParams from '@/helpers/useParams';
import auth from '@/auth';
import { useBunchQuery } from '@/queries/bunchQueries';
import { useQueryClient } from 'vue-query';
import { playersQueryKey } from '@/queries/playerQueries';

const params = useParams();
const route = useRoute();
const router = useRouter();
const bunchQuery = useBunchQuery(params.slug.value);
const queryClient = useQueryClient();

const code = ref('');
const errorMessage = ref<string>();

const routeCode = computed((): string | undefined => {
  return route.params.code as string | undefined;
});

const bunch = computed(() => bunchQuery.data.value!);
const bunchName = computed(() => bunch.value);

const ready = computed(() => {
  return bunchQuery.isSuccess.value;
});

const joinClicked = () => {
  join(bunch.value.id, code.value);
};

const join = async (bunchId: string, code: string) => {
  if (code.length > 0) {
    try {
      await api.joinBunch(bunchId, { code });
      queryClient.invalidateQueries(playersQueryKey(bunchId));
      router.push(urls.bunch.details(bunch.value.id));
    } catch (err) {
      const error = err as AxiosError<ApiError>;
      errorMessage.value = error.response?.data.message || 'Unknown Error';
    }
  }
};

const cancel = () => {
  router.push(urls.home);
};

watch(ready, () => {
  if (ready.value && routeCode.value) join(bunch.value.id, routeCode.value);
});

onMounted(() => auth.requireUser());
</script>
