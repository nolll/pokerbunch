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
          <ErrorMessage :message="errorMessage" />
          <p>
            <CustomButton v-on:click="saveMutation.mutate" type="action" text="Save" />
            <CustomButton v-on:click="cancel" text="Cancel" />
          </p>
        </Block>
        <Block v-else>
          <DefinitionList>
            <DefinitionTerm>Display Name</DefinitionTerm>
            <DefinitionData>{{ displayName }}</DefinitionData>
            <template v-if="canEdit">
              <DefinitionTerm>Real Name</DefinitionTerm>
              <DefinitionData>{{ realName }}</DefinitionData>
              <DefinitionTerm>Email</DefinitionTerm>
              <DefinitionData>{{ email }}</DefinitionData>
            </template>
          </DefinitionList>
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
import { Layout } from '@/components/Layouts';
import { Block, CustomButton, ErrorMessage, PageHeading, PageSection } from '@/components/Common';
import { User } from '@/models/User';
import { computed, onMounted, ref, watch } from 'vue';
import { useParams, useUser, useCurrentUser } from '@/composables';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import { userKey, userListKey } from '@/queries/queryKeys';
import { AxiosError } from 'axios';
import { ApiError } from '@/models/ApiError';
import { getErrorMessage } from '@/errors';
import { DefinitionList, DefinitionTerm, DefinitionData } from '../Common/DefinitionList';

const { userName } = useParams();
const { user, userReady } = useUser(userName.value);
const { currentUser, isAdmin } = useCurrentUser('');
const queryClient = useQueryClient();

const displayName = ref('');
const realName = ref('');
const email = ref('');
const isEditing = ref(false);
const errorMessage = ref('');

const canEdit = computed(() => isAdmin.value || isCurrentUser.value);
const isCurrentUser = computed(() => currentUser.value?.userName == user.value?.userName);
const canChangePassword = computed(() => isCurrentUser.value);
const ready = computed(() => userReady.value);
const avatarUrl = computed(() => user.value?.avatar);

const saveMutation = useMutation({
  mutationFn: async () => {
    errorMessage.value = '';

    var userToSave = {
      ...user.value,
      displayName: displayName.value,
      realName: realName.value,
      email: email.value,
    };

    await api.updateUser(userToSave);
  },
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: userListKey() });
    queryClient.invalidateQueries({ queryKey: userKey(user.value.userName) });
    isEditing.value = false;
  },
  onError: (error: AxiosError<ApiError>) => {
    errorMessage.value = getErrorMessage(error);
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
