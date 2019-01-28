<template>
    <two-column :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <template slot="aside">
            <page-section>
                <user-bunch-list />
            </page-section>
            <page-section v-if="isAdmin">
                <admin-navigation />
            </page-section>
        </template>

        <template slot="main">
            <page-section>
                <page-heading text="This is Poker Bunch" />
            </page-section>

            <div v-if="isSignedIn">
                <page-section>
                    <p>
                        Poker Bunch helps you keep track of the results in your poker homegames.
                        Please select one of your bunches, or <custom-link :url="addBunchUrl">create a new bunch</custom-link>.
                    </p>
                    <p>
                        If you want to join an existing bunch, you will need an invitation from a bunch player.
                    </p>
                </page-section>
                <page-section>
                    <h2 class="module-heading">Api</h2>
                    <p>
                        The <custom-link :url="apiDocsUrl">api</custom-link> makes it possible to create your own apps that interact with Poker Bunch.
                    </p>
                </page-section>
            </div>

            <div v-else>
                <page-section>
                    <p>
                        Poker Bunch helps you keep track of the results in your poker homegames.
                    </p>
                </page-section>
                <page-section>
                    <p>
                        <custom-link :url="loginUrl">Sign in</custom-link> if you already have an account, or
                        <custom-link :url="registerUrl">register</custom-link> to create a bunch and begin inviting players.
                    </p>
                </page-section>
            </div>
        </template>
    </two-column>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { DataMixin } from '@/mixins';
    import { TwoColumn } from '@/components/Layouts';
    import { AdminNavigation, BunchNavigation } from '@/components/Navigation';
    import { PageHeading, PageSection } from '@/components/Common';
    import urls from '@/urls';
    import { BUNCH, USER } from '@/store-names';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import UserBunchList from '@/components/UserBunchList/UserBunchList.vue';

    export default {
        components: {
            TwoColumn,
            UserBunchList,
            AdminNavigation,
            BunchNavigation,
            PageHeading,
            PageSection,
            CustomLink
        },
        mixins: [
            DataMixin
        ],
        computed: {
            ...mapGetters(BUNCH, [
                'slug'
            ]),
            ...mapGetters(USER, [
                'isSignedIn',
                'isAdmin'
            ]),
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
                return this.userReady && this.userBunchesReady;
            }
        },
        methods: {
            init() {
                this.loadUser();
                this.loadUserBunches();
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
