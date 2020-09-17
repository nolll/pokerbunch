<template>
    <nav>
        <h1 class="module-heading">Admin</h1>
        <simple-list>
            <simple-list-item>
                <custom-link :url="bunchesUrl"><span>Bunches</span></custom-link>
            </simple-list-item>
            <simple-list-item>
                <custom-link :url="usersUrl"><span>Users</span></custom-link>
            </simple-list-item>
            <simple-list-item>
                <custom-link :url="appsUrl"><span>Apps</span></custom-link>
            </simple-list-item>
            <simple-list-item>
                <custom-link :url="toolsUrl"><span>Tools</span></custom-link>
            </simple-list-item>
        </simple-list>
    </nav>
</template>

<script>
    import CustomLink from '@/components/Common/CustomLink.vue';
    import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
    import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
    import urls from '@/urls';
    import { BunchMixin } from '@/mixins'

    export default {
        components: {
            SimpleList,
            SimpleListItem,
            CustomLink
        },
        mixins: [
            BunchMixin
        ],
        computed: {
            bunchesUrl() {
                return urls.bunch.list;
            },
            usersUrl() {
                return urls.user.list;
            },
            appsUrl() {
                return urls.app.list;
            },
            toolsUrl() {
                return urls.admin.tools;
            },
            locationsUrl() {
                return urls.location.list(this.$_slug);
            }
        }
    };
</script>

<style>
</style>
