<template>
  <Layout :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <PageHeading text="Add Location" />
        </Block>

        <Block>
          <div class="field">
            <label class="label" for="location-name">Name</label>
            <input class="textfield" v-model="locationName" id="location-name" type="text" />
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
import useUsers from '@/composables/useUsers';
import { locationsQueryKey } from '@/queries/locationQueries';
import { useQueryClient } from 'vue-query';
import useParams from '@/helpers/useParams';
import api from '@/api';

const router = useRouter();
const users = useUsers();
const queryClient = useQueryClient();
const params = useParams();
const slug = computed(() => params.slug.value);

const locationName = ref('');

const init = () => {
  users.requireUser();
};

const add = async () => {
  if (locationName.value.length > 0) {
    await api.addLocation(slug.value, { name: locationName.value });
    queryClient.invalidateQueries(locationsQueryKey(slug.value));
    redirect();
  }
};

const cancel = () => {
  redirect();
};

const redirect = () => {
  router.push(urls.location.list(params.slug.value));
};

const ready = computed(() => true);

onMounted(() => {
  init();
});
</script>
