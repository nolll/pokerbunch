<template>
  <Layout :require-user="true" :ready="ready">
    <PageSection>
      <Block>
        <PageHeading text="Bunches" />
      </Block>

      <Block v-if="hasMessage">
        <p>
          {{ message }}
        </p>
      </Block>

      <Block>
        <SimpleList>
          <SimpleListItem v-for="bunch in bunches" :key="bunch.id">
            <div>
              {{ bunch.name }}
              <CustomButton text="Join" type="action" v-on:click="joinBunch(bunch.id)" />
            </div>
          </SimpleListItem>
        </SimpleList>
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import { Block, PageHeading, PageSection } from '@/components/Common';
import { computed, ref } from 'vue';
import { useBunchList } from '@/composables';
import { SimpleList, SimpleListItem } from '@/components/Common/SimpleList';
import { useMutation } from '@tanstack/vue-query';
import api from '@/api';
import { MessageResponse } from '@/response/MessageResponse';
import { ApiError } from '@/models/ApiError';
import { AxiosError } from 'axios';
import { CustomButton } from '@/components/Common';

const { bunches, bunchesReady } = useBunchList();

const joinBunch = async (id: string) => {
  joinBunchMutation.mutate({ id });
};

const joinBunchMutation = useMutation({
  mutationFn: async (params: { id: string }) => {
    const response = await api.addJoinRequest(params.id);
    return response.data;
  },
  onSuccess: (response: MessageResponse) => {
    showMessage(response.message);
  },
  onError: (err) => {
    const error = err as AxiosError<ApiError>;
    showMessage(error.response?.data.message || 'Unknown error');
  },
});

const showMessage = (m: string) => {
  message.value = m;
  console.log(hasMessage.value);
  setTimeout(clearMessage, 3000);
};
const message = ref<string | null>(null);
const hasMessage = computed(() => !!message.value);
const clearMessage = () => (message.value = null);

const ready = computed(() => bunchesReady.value);
</script>
