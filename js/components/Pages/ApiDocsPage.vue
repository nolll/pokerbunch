<template>
  <Layout :require-user="false" :ready="true">
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
        <pre><code>POST /login</code></pre>

        <p>Post the username and password as json:</p>
        <pre><code>{
  "username": "[username]",
  "password": "[password]"
}</code></pre>

        <p>If your credentials are valid, the response will include two tokens, for example</p>
        <pre><code>{
  "accessToken": "ABCD",
  "refreshToken": "EFGH"
}</code></pre>

        <p>For subsequent requests, include an Authorization header with the access token</p>
        <pre><code>bearer ABCDE</code></pre>

        <p>If your access token expires, you can get a new token from</p>
        <pre><code>POST /refresh</code></pre>

        <p>Post the refresh token like this:</p>
        <pre><code>{
  "refreshToken": "EFGH"
}</code></pre>
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import { Block, PageHeading, PageSection } from '@/components/Common';
import { computed } from 'vue';

const swaggerUrl = computed(() => 'https://api.pokerbunch.com/swagger');
</script>
