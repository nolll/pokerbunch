<template>
  <Layout :ready="ready">
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
import { computed, onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
import useParams from '@/helpers/useParams';
import auth from '@/auth';
import api from '@/api';
import { useQueryClient } from 'vue-query';
import { eventsQueryKey } from '@/queries/eventQueries';

const params = useParams();
const router = useRouter();
const queryClient = useQueryClient();

const eventName = ref('');

const slug = computed(() => params.slug.value);

const add = async () => {
  if (eventName.value.length > 0) {
    await api.addEvent(slug.value, { name: eventName.value });
    queryClient.invalidateQueries(eventsQueryKey(slug.value));
    redirect();
  }
};

const cancel = () => {
  redirect();
};

const redirect = () => {
  router.push(urls.event.list(slug.value));
};

const ready = computed(() => true);
onMounted(() => auth.requireUser());
</script>
