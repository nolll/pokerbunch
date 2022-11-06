<template>
  <Layout :ready="true">
    <PageSection>
      <Block>
        <PageHeading text="Api" />
      </Block>

      <Block>
        <p>
          You can build your own applications that interact with Poker Bunch, by using the Poker Bunch API. You'll find everything
          you need to know right here.
        </p>
      </Block>

      <Block>
        <h2 class="module-heading">Documentation</h2>
        <p>For detailed documentation, have a look at the <a :href="swaggerUrl" target="_blank">Swagger pages</a></p>
      </Block>

      <Block>
        <h2 class="module-heading">Authentication</h2>

        <p>To authenticate, your application needs to post a request to</p>
        <pre><code>POST token</code></pre>

        <p>Send the following parameters in the body:</p>
        <pre><code>grant_type = password
username = {your user's username}
password = {your user's password}</code></pre>

        <p>If your credentials are valid, the response will include a token, for example</p>
        <pre><code>ABCDE</code></pre>

        <p>For subsequent requests, include an Authorization header with the content</p>
        <pre><code>bearer ABCDE</code></pre>
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import Block from '@/components/Common/Block.vue';
import PageSection from '@/components/Common/PageSection.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import { computed } from 'vue';

const swaggerUrl = computed(() => {
  return `/api/swagger`;
});
</script>
