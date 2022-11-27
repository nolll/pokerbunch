<template>
  <Layout :ready="ready">
    <PageSection>
      <Block>
        <PageHeading text="Change Password" />
      </Block>

      <template v-if="isSaving">
        <Block>
          <p>Your password was changed</p>
        </Block>
        <Block>
          <CustomButton type="action" v-on:click="back" text="Back" />
        </Block>
      </template>
      <Block v-else>
        <p>
          <label class="label" for="oldPassword">Old Password</label>
          <input class="textfield" v-model="oldPassword" id="oldPassword" type="password" />
        </p>
        <p>
          <label class="label" for="newPassword">New Password</label>
          <input class="textfield" v-model="newPassword" id="newPassword" type="password" />
        </p>
        <p>
          <label class="label" for="repeat">Repeat New Password</label>
          <input class="textfield" v-model="repeat" id="repeat" type="password" />
        </p>
        <p v-if="hasError" class="validation-error">
          {{ errorMessage }}
        </p>
        <p>
          <CustomButton v-on:click="save" type="action" text="Save" />
          <CustomButton v-on:click="back" text="Cancel" />
        </p>
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import urls from '@/urls';
import api from '@/api';
import Layout from '@/components/Layouts/Layout.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import { AxiosError } from 'axios';
import { ApiError } from '@/models/ApiError';
import { ApiParamsChangePassword } from '@/models/ApiParamsChangePassword';
import useUsers from '@/composables/useUsers';
import { computed, onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const users = useUsers();

const oldPassword = ref('');
const newPassword = ref('');
const repeat = ref('');
const errorMessage = ref('');
const isSaving = ref(false);

const ready = computed(() => {
  return users.userReady.value;
});

const hasError = computed(() => {
  return !!errorMessage.value;
});

const save = async () => {
  errorMessage.value = '';

  if (repeat.value !== newPassword.value) {
    errorMessage.value = "Passwords doesn't match";
    return;
  }

  try {
    const params: ApiParamsChangePassword = {
      oldPassword: oldPassword.value,
      newPassword: newPassword.value,
    };
    const response = await api.changePassword(params);
    isSaving.value = true;
  } catch (err) {
    const error = err as AxiosError<ApiError>;
    const message = error.response?.data.message || 'Unknown Error';
    errorMessage.value = message;
  }
};

const back = () => {
  router.push(urls.user.details(users.userName.value));
};

const init = () => {
  users.requireUser();
};

onMounted(() => {
  init();
});
</script>
