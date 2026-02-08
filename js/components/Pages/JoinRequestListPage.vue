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
        <DataTable size="small" :value="joinRequests" responsiveLayout="scroll">
          <Column field="userName" header="Username"></Column>
          <Column>
            <template #body="{ data }">
              <div class="actions-column">
                <Button
                  v-on:click="() => deny(data)"
                  variant="outlined"
                  severity="danger"
                  size="small"
                  label="Deny"
                  icon="pi pi-times"
                ></Button>
                <Button
                  v-on:click="() => accept(data)"
                  variant="outlined"
                  severity="success"
                  size="small"
                  label="Accept"
                  icon="pi pi-check"
                ></Button>
              </div>
            </template>
          </Column>
        </DataTable>
      </Block>
      <Block v-else>
        <p>There are no join requests.</p>
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import { Block, PageHeading, PageSection } from '@/components/Common';
import { computed, ref } from 'vue';
import { useJoinRequestList, useParams } from '@/composables';
import { JoinRequestResponse } from '@/response/JoinRequestResponse';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import api from '@/api';
import { MessageResponse } from '@/response/MessageResponse';
import { AxiosError } from 'axios';
import { ApiError } from '@/models/ApiError';
import { joinRequestListKey } from '@/queries/queryKeys';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import { Button } from 'primevue';

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

<style lang="scss" scoped>
.actions-column {
  display: flex;
  gap: 0.5rem;
  justify-content: right;
}
</style>
