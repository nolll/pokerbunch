<template>
  <Layout :ready="ready">
    <PageSection>
      <template v-slot:aside1>
        <Block>
          <img :src="avatarUrl" alt="User avatar" />
        </Block>
      </template>

      <template v-slot:default>
        <Block>
          <PageHeading :text="userName" />
        </Block>

        <Block v-if="isEditing">
          <p>
            <label class="label" for="display-name">Display Name</label>
            <input class="textfield" v-model="displayName" id="display-name" type="text" />
          </p>
          <p>
            <label class="label" for="real-name">Real Name</label>
            <input class="textfield" v-model="realName" id="real-name" type="text" />
          </p>
          <p>
            <label class="label" for="email">Email</label>
            <input class="textfield" v-model="email" id="email" type="text" />
          </p>
          <p v-if="hasError" class="validation-error">
            {{ errorMessage }}
          </p>
          <p>
            <CustomButton v-on:click="save" type="action" text="Save" />
            <CustomButton v-on:click="cancel" text="Cancel" />
          </p>
        </Block>
        <Block v-else>
          <ValueList>
            <ValueListKey>Display Name</ValueListKey>
            <ValueListValue>{{ displayName }}</ValueListValue>
            <template v-if="canEdit">
              <ValueListKey>Real Name</ValueListKey>
              <ValueListValue>{{ realName }}</ValueListValue>
              <ValueListKey>Email</ValueListKey>
              <ValueListValue>{{ email }}</ValueListValue>
            </template>
          </ValueList>
        </Block>

        <Block v-if="canEdit && !isEditing">
          <CustomButton type="action" v-on:click="edit" text="Edit" />
          <CustomButton type="action" url="/user/changepassword" text="Change Password" v-if="canChangePassword" />
        </Block>
      </template>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import api from '@/api';
import Layout from '@/components/Layouts/Layout.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import { User } from '@/models/User';
import ValueList from '@/components/Common/ValueList/ValueList.vue';
import ValueListKey from '@/components/Common/ValueList/ValueListKey.vue';
import ValueListValue from '@/components/Common/ValueList/ValueListValue.vue';
import { AxiosError } from 'axios';
import { ApiError } from '@/models/ApiError';
import useUsers from '@/composables/useUsers';
import { useRoute } from 'vue-router';
import { computed, onMounted, ref, watch } from 'vue';

const route = useRoute();
const users = useUsers();

const user = ref<User>();
const userReady = ref(false);
const displayName = ref('');
const realName = ref('');
const email = ref('');
const isEditing = ref(false);
const errorMessage = ref('');

const canEdit = computed(() => {
  return users.isAdmin.value || isCurrentUser.value;
});

const isCurrentUser = computed(() => {
  return users.userName.value == user.value?.userName;
});

const canChangePassword = computed(() => {
  return isCurrentUser.value;
});

const ready = computed(() => {
  return users.userReady.value && userReady.value;
});

const userName = computed(() => {
  return user.value?.userName ?? '';
});

const avatarUrl = computed(() => {
  return user.value?.avatar;
});

const hasError = computed(() => {
  return !!errorMessage.value;
});

const loadUser = async () => {
  const response = await api.getUser(route.params.userName as string);
  user.value = response.data;
  if (user.value) {
    setMembers(user.value);
  }
  userReady.value = true;
};

const save = async () => {
  errorMessage.value = '';

  if (!user.value) {
    isEditing.value = false;
    return;
  }

  user.value.displayName = displayName.value;
  user.value.realName = realName.value;
  user.value.email = email.value;
  try {
    const response = await api.updateUser(user.value);
    isEditing.value = false;
  } catch (err) {
    const error = err as AxiosError<ApiError>;
    const message = error.response?.data.message || 'Unknown Error';
    errorMessage.value = message;
  }
};

const cancel = () => {
  if (user.value) setMembers(user.value);
  isEditing.value = false;
};

const setMembers = (user: User) => {
  displayName.value = user.displayName;
  realName.value = user.realName || '';
  email.value = user.email || '';
};

const edit = () => {
  isEditing.value = true;
};

const init = () => {
  users.requireUser();
  loadUser();
};

onMounted(() => {
  init();
});

watch(route, () => {
  init();
});
</script>
