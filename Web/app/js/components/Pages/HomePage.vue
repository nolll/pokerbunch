<template>
    <layout :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <page-section>
            <block>
                <page-heading text="This is Poker Bunch" />
            </block>

            <div v-if="$_isSignedIn">
                <block>
                    <p>
                        Poker Bunch helps you keep track of the results in your poker homegames.
                        Please select one of your bunches, or <custom-link :url="addBunchUrl">create a new bunch</custom-link>.
                    </p>
                    <p>
                        If you want to join an existing bunch, you will need an invitation from a bunch player.
                    </p>
                </block>
                <block>
                    <h2 class="module-heading">Api</h2>
                    <p>
                        The <custom-link :url="apiDocsUrl">api</custom-link> makes it possible to create your own apps that interact with Poker Bunch.
                    </p>
                </block>
            </div>

            <div v-else>
                <block>
                    <p>
                        Poker Bunch helps you keep track of the results in your poker homegames.
                    </p>
                </block>
                <block>
                    <p>
                        <custom-link :url="loginUrl">Sign in</custom-link> if you already have an account, or
                        <custom-link :url="registerUrl">register</custom-link> to create a bunch and begin inviting players.
                    </p>
                </block>
            </div>

            <template slot="aside2">
                <block>
                    <user-bunch-list />
                </block>
                <block v-if="$_isAdmin">
                    <admin-navigation />
                </block>
            </template>

        </page-section>
    </layout>
</template>

<script>
    import { BunchMixin, UserMixin } from '@/mixins';
    import { Layout } from '@/components/Layouts';
    import { AdminNavigation, BunchNavigation } from '@/components/Navigation';
    import { Block, PageHeading, PageSection } from '@/components/Common';
    import urls from '@/urls';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import UserBunchList from '@/components/UserBunchList/UserBunchList.vue';

    export default {
        components: {
            Layout,
            UserBunchList,
            AdminNavigation,
            BunchNavigation,
            Block,
            PageHeading,
            PageSection,
            CustomLink
        },
        mixins: [
            BunchMixin,
            UserMixin
        ],
        computed: {
            loginUrl() {
                return urls.auth.login;
            },
            registerUrl() {
                return urls.user.add;
            },
            addBunchUrl() {
                return urls.bunch.add;
            },
            apiDocsUrl() {
                return urls.api.docs;
            },
            ready() {
                return this.$_userReady && this.$_userBunchesReady;
            }
        },
        methods: {
            init() {
                this.$_loadUser();
                this.$_loadUserBunches();
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
