<template>
  <Layout :require-user="true" :ready="true">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <PageHeading text="Add Event" />
        </Block>

        <Block>
          <div class="field">
            <label class="label" for="event-name">Name</label>
            <input class="textfield" v-model="eventName" id="event-name" type="text" />
          </div>
          <ErrorMessage :message="errorMessage" />
          <div class="buttons">
            <CustomButton v-on:click="add" type="action" text="Add" />
            <CustomButton v-on:click="cancel" text="Cancel" />
          </div>
        </Block>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import { Block, CustomButton, ErrorMessage, PageHeading, PageSection } from '@/components/Common';
import urls from '@/urls';
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import useParams from '@/composables/useParams';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import api from '@/api';
import { eventListKey } from '@/queries/queryKeys';

const queryClient = useQueryClient();
const { slug } = useParams();
const router = useRouter();
const eventName = ref('');
const errorMessage = ref('');

const redirect = () => {
  router.push(urls.event.list(slug.value));
};

const addEventMutation = useMutation({
  mutationFn: async () => {
    const response = await api.addEvent(slug.value, {
      name: eventName.value ?? '',
    });
    return response.data;
  },
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: eventListKey(slug.value) });
    redirect();
  },
  onError: () => {
    errorMessage.value = 'Server error';
  },
});

const add = () => {
  if (eventName.value.length > 0) {
    addEventMutation.mutateAsync();
  } else {
    errorMessage.value = "Name can't be empty";
  }
};

const cancel = () => redirect();
</script>
