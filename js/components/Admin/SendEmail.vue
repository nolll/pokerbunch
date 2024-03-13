<template>
  <div>
    <h2 class="module-heading">Send test email</h2>
    <p>
      <CustomButton text="Send" type="action" v-on:click="sendEmail" />
    </p>
    <p v-if="hasMessage">
      {{ message }}
    </p>
  </div>
</template>

<script setup lang="ts">
import CustomButton from '@/components/Common/CustomButton.vue';
import { useSendTestEmailMutation } from '@/mutations/sendTestEmailMutation';
import { MessageResponse } from '@/response/MessageResponse';
import { computed, ref } from 'vue';

const showMessage = (m: string) => {
  message.value = m;
  setTimeout(clearMessage, 3000);
};

const onComplete = (response: MessageResponse) => showMessage(response.message);
const { mutateAsync: sendEmail } = useSendTestEmailMutation(onComplete);
const message = ref<string | null>(null);
const hasMessage = computed(() => !!message.value);
const clearMessage = () => (message.value = null);
</script>
