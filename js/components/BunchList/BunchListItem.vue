<template>
  <div>
    {{ name }}
    <CustomButton text="Join" type="action" v-on:click="joinBunch" />
  </div>
</template>

<script setup lang="ts">
import urls from '@/urls';
import { CustomButton } from '@/components/Common';
import { computed } from 'vue';
import { useMutation } from '@tanstack/vue-query';
import api from '@/api';
import { MessageResponse } from '@/response/MessageResponse';
import { AxiosError } from 'axios';
import { ApiError } from '@/models/ApiError';

const props = defineProps<{
  id: string;
  name: string;
}>();

const url = computed(() => {
  return urls.bunch.details(props.id);
});

const joinBunch = async () => {
  joinBunchMutation.mutate();
};

const joinBunchMutation = useMutation({
  mutationFn: async () => {
    const response = await api.addJoinRequest(props.id);
    return response.data;
  },
  onSuccess: (response: MessageResponse) => {
    console.log(response.message);
  },
  onError: (err) => {
    const error = err as AxiosError<ApiError>;
    console.log(error.response?.data.message || 'Unknown Error');
  },
});
</script>
