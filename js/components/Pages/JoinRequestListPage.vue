<template>
  <Layout :require-user="true" :ready="ready">
    <PageSection>
      <Block>
        <PageHeading text="Join requests" />
      </Block>

      <Block>
        <SimpleList>
          <SimpleListItem v-for="joinRequest in joinRequests" :key="joinRequest.id">
            {{ joinRequest.userName }}
            <CustomButton text="Deny" type="action" v-on:click="() => deny(joinRequest)" />
            <CustomButton text="Accept" type="action" v-on:click="() => accept(joinRequest)" />
          </SimpleListItem>
        </SimpleList>
      </Block>
    </PageSection>
  </Layout>
</template>

<script setup lang="ts">
import { Layout } from '@/components/Layouts';
import { Block, PageHeading, PageSection, CustomButton } from '@/components/Common';
import { SimpleList, SimpleListItem } from '@/components/Common/SimpleList';
import { computed } from 'vue';
import { useJoinRequestList, useParams } from '@/composables';
import { JoinRequestResponse } from '@/response/JoinRequestResponse';

const { slug } = useParams();
const { joinRequests, joinRequestsReady } = useJoinRequestList(slug.value);

const accept = (joinRequest: JoinRequestResponse) => {
  console.log('accept join request', joinRequest);
};

const deny = (joinRequest: JoinRequestResponse) => {
  console.log('deny join request', joinRequest);
};

const ready = computed(() => joinRequestsReady.value);
</script>
