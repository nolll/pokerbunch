<template>
    <layout :ready="ready">
        <page-section>
            <block>
                <page-heading text="Users" />
            </block>
            <block>
                <user-list />
            </block>
        </page-section>
    </layout>
</template>

<script>
    import { UserMixin } from '@/mixins';
    import { Layout } from '@/components/Layouts';
    import { Block, PageHeading, PageSection } from '@/components/Common';
    import UserList from '@/components/UserList/UserList.vue';

    export default {
        components: {
            Layout,
            Block,
            PageHeading,
            PageSection,
            UserList
        },
        mixins: [
            UserMixin
        ],
        computed: {
            ready() {
                return this.$_userReady && this.$_usersReady;
            }
        },
        methods: {
            init() {
                this.$_requireUser();
                this.$_loadUsers();
            }
        },
        watch: {
            '$route'(to, from) {
                this.init();
            }
        },
        mounted: function () {
            this.init();
        }
    };
</script>

<style>
</style>
