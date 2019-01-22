<template>
    <two-column :ready="ready">
        <template slot="main">
            <div class="block gutter">
                <page-heading text="Users"/>
            </div>
            <div class="block gutter">
                <user-list />
            </div>
        </template>
    </two-column>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { DataMixin } from '@/mixins';
    import { TwoColumn } from '@/components/Layouts';
    import { PageHeading } from '@/components/Common';
    import UserList from '@/components/UserList/UserList.vue';
    import { USER } from '@/store-names';

    export default {
        components: {
            TwoColumn,
            PageHeading,
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
