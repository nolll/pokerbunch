<template>
  <Layout :ready="ready">
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
            <PlayerList :players="players" :slug="slug" />
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
import { computed, onMounted } from 'vue';
import useParams from '@/helpers/useParams';
import auth from '@/auth';
import { usePlayersQuery } from '@/queries/playerQueries';

const params = useParams();
const slug = computed(() => params.slug.value);
const playersQuery = usePlayersQuery(slug.value);
const addPlayerUrl = computed(() => urls.player.add(slug.value));

const players = computed(() => playersQuery.data.value!);

const ready = computed(() => {
  return playersQuery.isSuccess.value;
});

onMounted(() => auth.requireUser());
</script>
