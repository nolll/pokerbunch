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
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import Block from '@/components/Common/Block.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import urls from '@/urls';
import { ref } from 'vue';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import { useRouter } from 'vue-router';
import useParams from '@/composables/useParams';
import api from '@/api';
import { eventListKey } from '@/queries/queryKeys';

const { slug } = useParams();
const router = useRouter();
const queryClient = useQueryClient();

const eventName = ref('');

const addEvent = async () => {
  await api.addEvent(slug.value, { name: eventName.value });
};

const addMutation = useMutation({
  mutationFn: addEvent,
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: eventListKey(slug.value) });
    redirect();
  },
});

const add = () => {
  if (eventName.value.length > 0) {
    addMutation.mutate();
    redirect();
  }
};

const cancel = () => {
  redirect();
};

const redirect = () => {
  router.push(urls.event.list(slug.value));
};
</script>
