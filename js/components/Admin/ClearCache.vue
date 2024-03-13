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
import CustomButton from '@/components/Common/CustomButton.vue';
import { useClearCacheMutation } from '@/mutations/clearCacheMutation';
import { MessageResponse } from '@/response/MessageResponse';
import { computed, ref } from 'vue';

const showMessage = (m: string) => {
  message.value = m;
  setTimeout(clearMessage, 3000);
};
const onComplete = (response: MessageResponse) => showMessage(response.message);
const { mutateAsync: clearCache } = useClearCacheMutation(onComplete);
const message = ref<string | null>(null);
const hasMessage = computed(() => !!message.value);
const clearMessage = () => (message.value = null);
</script>
