<template>
  <Layout :require-user="false" :ready="true">
    <PageSection>
      <Block>
        <PageHeading text="Page not found" />
      </Block>
      <Block>
        <p>Please check the url for errors</p>
        <p>Go to <CustomLink :url="homeUrl"></CustomLink></p>
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import urls from '@/urls';
import { Layout } from '@/components/Layouts';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import CustomLink from '@/components/Common/CustomLink.vue';
import { computed } from 'vue';

const homeUrl = computed(() => {
  return urls.home;
});
</script>
