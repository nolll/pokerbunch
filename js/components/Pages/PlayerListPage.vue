<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <template v-slot:aside1>
          <Block>
            <CustomButton :url="addPlayerUrl" type="action" text="Add player" />
          </Block>
        </template>

        <template v-slot:default>
          <Block>
            <PageHeading text="Players" />
          </Block>

          <Block>
            <PlayerList :bunchId="slug" :players="players" />
          </Block>
        </template>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import PlayerList from '@/components/PlayerList/PlayerList.vue';
import Block from '@/components/Common/Block.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import urls from '@/urls';
import { computed } from 'vue';
import useParams from '@/composables/useParams';
import usePlayerList from '@/composables/usePlayerList';

const { slug } = useParams();
const { players, playersReady } = usePlayerList(slug.value);

const addPlayerUrl = computed(() => {
  return urls.player.add(slug.value);
});

const ready = computed(() => {
  return playersReady.value;
});
</script>
