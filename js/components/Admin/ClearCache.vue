<template>
  <div>
    <h2 class="module-heading">Clear cache</h2>
    <p>
      <CustomButton text="Clear" type="action" v-on:click="clearCache" />
    </p>
    <p v-if="hasMessage">
      {{ message }}
    </p>
  </div>
</template>

<script setup lang="ts">
import api from '@/api';
import CustomButton from '@/components/Common/CustomButton.vue';
import { MessageResponse } from '@/response/MessageResponse';
import { useMutation } from '@tanstack/vue-query';
import { computed, ref } from 'vue';

const showMessage = (m: string) => {
  message.value = m;
  setTimeout(clearMessage, 3000);
};
const onComplete = (response: MessageResponse) => showMessage(response.message);
const message = ref<string | null>(null);
const hasMessage = computed(() => !!message.value);
const clearMessage = () => (message.value = null);

const { mutateAsync: clearCache } = useMutation({
  mutationFn: async (): Promise<MessageResponse> => {
    var response = await api.clearCache();
    return response.data;
  },
  onSuccess: onComplete,
  onError: onComplete,
});
</script>
