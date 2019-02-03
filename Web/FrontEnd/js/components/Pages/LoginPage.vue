<template>
    <layout :ready="ready">
        <page-section>
            <block>
                <page-heading text="Sign in" />
                <p>
                    Please sign in using your username and password. <custom-link :url="forgotPasswordUrl">Forgot password?</custom-link>
                </p>
                <p>
                    If you are a new user, please <custom-link :url="registerUrl">register</custom-link>!
                </p>
                <login-form />
            </block>
        </page-section>
    </layout>
</template>

<script>
    import { DataMixin } from '@/mixins';
    import { Layout } from '@/components/Layouts';
    import { LoginForm } from '@/components';
    import { CustomLink, Block, PageHeading, PageSection } from '@/components/Common';
    import urls from '@/urls';

    export default {
        components: {
            Layout,
            LoginForm,
            CustomLink,
            Block,
            PageHeading,
            PageSection
        },
        mixins: [
            DataMixin
        ],
        computed: {
            forgotPasswordUrl() {
                return urls.user.forgotPassword;
            },
            registerUrl() {
                return urls.user.add;
            },
            ready() {
                return this.userReady;
            }
        },
        methods: {
            redirectIfSignedIn() {
                if (this.userReady && this.isSignedIn) {
                    this.$router.push(urls.home);
                }
            },
            init() {
                this.loadUser();
            }
        },
        watch: {
            userReady() {
                this.redirectIfSignedIn();
            }
        },
        mounted() {
            this.init();
            this.redirectIfSignedIn();
        }
    };
</script>

<style>
</style>
