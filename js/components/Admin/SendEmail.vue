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

const onComplete = (response: MessageResponse) => {
  message.value = response.message;
  setTimeout(clearMessage, 3000);
};

const { mutateAsync: sendEmail } = useSendTestEmailMutation(onComplete, onComplete);
const message = ref<string | null>(null);
const hasMessage = computed(() => !!message.value);
const clearMessage = () => (message.value = null);
</script>
