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
import { Block, CustomLink, PageHeading, PageSection } from '@/components/Common';
import { computed } from 'vue';

const homeUrl = computed(() => {
  return urls.home;
});
</script>
