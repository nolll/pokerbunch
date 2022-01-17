<template>
  <div>
    <h2 class="module-heading">Clear cache</h2>
    <p>
      <CustomButton text="Clear" :type="buttonType" v-on:click="clearCache" />
    </p>
    <p v-if="hasMessage">
      {{ message }}
    </p>
  </div>
</template>

<script setup lang="ts">
import api from '@/api';
import CustomButton from '@/components/Common/CustomButton.vue';
import { ButtonType } from '@/models/ButtonType';
import { computed, ref } from 'vue';

const message = ref<string | null>(null);
const buttonType = ButtonType.Action;

const hasMessage = computed(() => {
  return !!message.value;
});

const clearCache = async () => {
  var response = await api.clearCache();
  message.value = response.data.message;
  setTimeout(clearMessage, 3000);
};

const clearMessage = () => {
  message.value = null;
};
</script>
