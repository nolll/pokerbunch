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
    import { mapGetters } from 'vuex';
    import { DataMixin } from '@/mixins';
    import { Layout } from '@/components/Layouts';
    import { Block, PageHeading, PageSection } from '@/components/Common';
    import UserList from '@/components/UserList/UserList.vue';
    import { USER } from '@/store-names';

    export default {
        components: {
            Layout,
            Block,
            PageHeading,
            PageSection,
            UserList
        },
        mixins: [
            DataMixin
        ],
        computed: {
            ...mapGetters(USER, [
                'users'
            ]),
            ready() {
                return this.userReady && this.usersReady;
            }
        },
        methods: {
            init() {
                this.loadUser();
                this.loadUsers();
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
