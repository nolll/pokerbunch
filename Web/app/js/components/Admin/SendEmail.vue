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
import api from '@/api';
import CustomButton from '@/components/Common/CustomButton.vue';
import { computed, ref } from 'vue';

const message = ref<string | null>(null);

const hasMessage = computed(() => {
  return !!message.value;
});

const sendEmail = async () => {
  var response = await api.sendEmail();
  message.value = response.data.message;
  setTimeout(clearMessage, 3000);
};

const clearMessage = () => {
  message.value = null;
};
</script>
