<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <Block>
                <PageHeading text="This is Poker Bunch" />
            </Block>

            <div v-if="$_isSignedIn">
                <Block>
                    <p>
                        Poker Bunch helps you keep track of the results in your poker homegames.
                        Please select one of your bunches, or <CustomLink :url="addBunchUrl">create a new bunch</CustomLink>.
                    </p>
                    <p>
                        If you want to join an existing bunch, you will need an invitation from a bunch player.
                    </p>
                </Block>
                <Block>
                    <h2 class="module-heading">Api</h2>
                    <p>
                        The <CustomLink :url="apiDocsUrl">api</CustomLink> makes it possible to create your own apps that interact with Poker Bunch.
                    </p>
                </Block>
            </div>

            <div v-else>
                <Block>
                    <p>
                        Poker Bunch helps you keep track of the results in your poker homegames.
                    </p>
                </Block>
                <Block>
                    <p>
                        <CustomLink :url="loginUrl">Sign in</CustomLink> if you already have an account, or
                        <CustomLink :url="registerUrl">register</CustomLink> to create a bunch and begin inviting players.
                    </p>
                </Block>
            </div>

            <template slot="aside2">
                <Block>
                    <UserBunchList />
                </Block>
                <Block v-if="$_isAdmin">
                    <AdminNavigation />
                </Block>
            </template>

        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, UserMixin } from '@/mixins';
    import Layout from '@/components/Layouts/Layout.vue';
    import AdminNavigation from '@/components/Navigation/AdminNavigation.vue';
    import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
    import Block from '@/components/Common/Block.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import urls from '@/urls';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import UserBunchList from '@/components/UserBunchList/UserBunchList.vue';

    @Component({
        components: {
            Layout,
            UserBunchList,
            AdminNavigation,
            BunchNavigation,
            Block,
            PageHeading,
            PageSection,
            CustomLink
        }
    })
    export default class HomePage extends Mixins(
        BunchMixin,
        UserMixin
    ) {
        get loginUrl() {
            return urls.auth.login;
        }

        get registerUrl() {
            return urls.user.add;
        }

        get addBunchUrl() {
            return urls.bunch.add;
        }

        get apiDocsUrl() {
            return urls.api.docs;
        }

        get ready() {
            return this.$_userReady && this.$_userBunchesReady;
        }

        init() {
            this.$_loadCurrentUser();
            this.$_loadUserBunches();
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
