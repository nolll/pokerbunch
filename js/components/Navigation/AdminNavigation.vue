<template>
  <nav>
    <h1 class="module-heading">Admin</h1>
    <SimpleList>
      <SimpleListItem>
        <CustomLink :url="bunchesUrl"><span>Bunches</span></CustomLink>
      </SimpleListItem>
      <SimpleListItem>
        <CustomLink :url="usersUrl"><span>Users</span></CustomLink>
      </SimpleListItem>
      <SimpleListItem>
        <CustomLink :url="toolsUrl"><span>Tools</span></CustomLink>
      </SimpleListItem>
    </SimpleList>
  </nav>
</template>

<script setup lang="ts">
import { CustomLink } from '@/components/Common';
import { SimpleList, SimpleListItem } from '@/components/Common/SimpleList';
import urls from '@/urls';
import { computed } from 'vue';

const bunchesUrl = computed(() => {
  return urls.bunch.list;
});

const usersUrl = computed(() => {
  return urls.user.list;
});

const toolsUrl = computed(() => {
  return urls.admin.tools;
});
</script>
