<template>
  <Layout :require-user="true" :ready="ready">
    <PageSection>
      <Block>
        <PageHeading text="Join requests" />
      </Block>

      <Block v-if="hasMessage">
        <p>
          {{ message }}
        </p>
      </Block>

      <Block v-if="hasJoinRequests">
        <SimpleList>
          <SimpleListItem v-for="joinRequest in joinRequests" :key="joinRequest.id">
            {{ joinRequest.userName }}
            <CustomButton text="Deny" type="action" v-on:click="() => deny(joinRequest)" />
            <CustomButton text="Accept" type="action" v-on:click="() => accept(joinRequest)" />
          </SimpleListItem>
        </SimpleList>
      </Block>
      <Block v-else>
        <p>There are no join requests.</p>
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import { Block, PageHeading, PageSection, CustomButton } from '@/components/Common';
import { SimpleList, SimpleListItem } from '@/components/Common/SimpleList';
import { computed, ref } from 'vue';
import { useJoinRequestList, useParams } from '@/composables';
import { JoinRequestResponse } from '@/response/JoinRequestResponse';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import api from '@/api';
import { MessageResponse } from '@/response/MessageResponse';
import { AxiosError } from 'axios';
import { ApiError } from '@/models/ApiError';
import { joinRequestListKey } from '@/queries/queryKeys';

const { slug } = useParams();
const { joinRequests, joinRequestsReady } = useJoinRequestList(slug.value);
const queryClient = useQueryClient();

const hasJoinRequests = computed(() => joinRequests.value.length > 0);

const accept = (joinRequest: JoinRequestResponse) => {
  acceptMutation.mutate({ id: joinRequest.id });
};

const deny = (joinRequest: JoinRequestResponse) => {
  denyMutation.mutate({ id: joinRequest.id });
};

const acceptMutation = useMutation({
  mutationFn: async (params: { id: string }) => {
    const response = await api.acceptJoinRequest(params.id);
    return response.data;
  },
  onSuccess: (response: MessageResponse) => {
    queryClient.invalidateQueries({ queryKey: joinRequestListKey(slug.value) });
    showMessage(response.message);
  },
  onError: (err) => {
    const error = err as AxiosError<ApiError>;
    showMessage(error.response?.data.message || 'Unknown Error');
  },
});

const denyMutation = useMutation({
  mutationFn: async (params: { id: string }) => {
    const response = await api.denyJoinRequest(params.id);
    return response.data;
  },
  onSuccess: (response: MessageResponse) => {
    queryClient.invalidateQueries({ queryKey: joinRequestListKey(slug.value) });
    showMessage(response.message);
  },
  onError: (err) => {
    const error = err as AxiosError<ApiError>;
    showMessage(error.response?.data.message || 'Unknown Error');
  },
});

const showMessage = (m: string) => {
  message.value = m;
  setTimeout(clearMessage, 3000);
};
const message = ref<string | null>(null);
const hasMessage = computed(() => !!message.value);
const clearMessage = () => (message.value = null);

const ready = computed(() => joinRequestsReady.value);
</script>
