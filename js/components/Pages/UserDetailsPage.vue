<template>
  <Layout :require-user="true" :ready="ready">
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
            <CustomButton v-on:click="saveMutation.mutate" type="action" text="Save" />
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
import { computed, onMounted, ref, watch } from 'vue';
import useParams from '@/composables/useParams';
import useUser from '@/composables/useUser';
import useCurrentUser from '@/composables/useCurrentUser';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import { userKey, userListKey } from '@/queries/queryKeys';

const params = useParams();
const { user, userReady } = useUser(params.userName.value);
const { currentUser, isAdmin, currentUserReady } = useCurrentUser();
const queryClient = useQueryClient();

const displayName = ref('');
const realName = ref('');
const email = ref('');
const isEditing = ref(false);
const errorMessage = ref('');

const canEdit = computed(() => {
  return isAdmin.value || isCurrentUser.value;
});

const isCurrentUser = computed(() => {
  return currentUser.value?.userName == user.value?.userName;
});

const canChangePassword = computed(() => {
  return isCurrentUser.value;
});

const ready = computed(() => {
  return currentUserReady.value && userReady.value;
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

const save = async () => {
  errorMessage.value = '';

  var userToSave = {
    ...user.value,
    displayName: displayName.value,
    realName: realName.value,
    email: email.value,
  };

  await api.updateUser(userToSave);
};

const saveMutation = useMutation({
  mutationFn: save,
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: userListKey() });
    queryClient.invalidateQueries({ queryKey: userKey(user.value.userName) });
    isEditing.value = false;
  },
  onError: (error) => {
    errorMessage.value = error.message;
  },
});

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

watch(user, (u) => {
  setMembers(u);
});

onMounted(() => {
  if (user.value) setMembers(user.value);
});
</script>
