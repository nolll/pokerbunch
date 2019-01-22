<template>
    <two-column :ready="ready">
        <template slot="main">
            <page-section>
                <page-heading text="Users"/>
            </page-section>
            <page-section>
                <user-list />
            </page-section>
        </template>
    </two-column>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { DataMixin } from '@/mixins';
    import { TwoColumn } from '@/components/Layouts';
    import { PageHeading, PageSection } from '@/components/Common';
    import UserList from '@/components/UserList/UserList.vue';
    import { USER } from '@/store-names';

    export default {
        components: {
            TwoColumn,
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
        created: function () {
            this.init();
        }
    };
</script>

<style>

</style>
