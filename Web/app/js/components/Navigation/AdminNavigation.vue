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
                <CustomLink :url="appsUrl"><span>Apps</span></CustomLink>
            </SimpleListItem>
            <SimpleListItem>
                <CustomLink :url="toolsUrl"><span>Tools</span></CustomLink>
            </SimpleListItem>
        </SimpleList>
    </nav>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
    import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
    import urls from '@/urls';
    import { BunchMixin } from '@/mixins'

    @Component({
        components: {
            SimpleList,
            SimpleListItem,
            CustomLink
        }
    })
    export default class AdminNavigation extends Mixins(
        BunchMixin
    ) {
        get bunchesUrl() {
            return urls.bunch.list;
        }

        get usersUrl() {
            return urls.user.list;
        }

        get appsUrl() {
            return urls.app.list;
        }

        get toolsUrl() {
            return urls.admin.tools;
        }

        get locationsUrl() {
            return urls.location.list(this.$_slug);
        }
    }
</script>
