<template>
    <Layout :ready="ready">
        <PageSection>
            
            <Block>
                <PageHeading text="Users" />
            </Block>

            <Block v-if="isAdmin">
                <UserList />
            </Block>

            <Block v-else>
                Access denied
            </Block>

        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { UserMixin } from '@/mixins';
    import Layout from '@/components/Layouts/Layout.vue';
    import Block from '@/components/Common/Block.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import UserList from '@/components/UserList/UserList.vue';

    @Component({
        components: {
            Layout,
            Block,
            PageHeading,
            PageSection,
            UserList
        }
    })
    export default class UserListPage extends Mixins(
        UserMixin
    ) {
        get ready() {
            return this.$_userReady && this.$_usersReady;
        }

        get isAdmin(){
            return this.$_isAdmin;
        }

        init() {
            this.$_requireUser();
            this.$_loadUsers();
        }

        mounted() {
            this.init();
        }

        @Watch('$route')
        routeChanged() {
            this.init();
        }
    }
</script>
