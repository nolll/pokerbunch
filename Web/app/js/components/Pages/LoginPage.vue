<template>
    <Layout :ready="ready">
        <PageSection>
            <Block>
                <PageHeading text="Sign in" />
                <p>
                    Please sign in using your username and password. <CustomLink :url="resetPasswordUrl">Forgot password?</CustomLink>
                </p>
                <p>
                    If you are a new user, please <CustomLink :url="registerUrl">register</CustomLink>!
                </p>
                <LoginForm />
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { UserMixin } from '@/mixins';
    import Layout from '@/components/Layouts/Layout.vue';
    import LoginForm from '@/components/LoginForm.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import Block from '@/components/Common/Block.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import urls from '@/urls';
    
    @Component({
        components: {
            Layout,
            LoginForm,
            CustomLink,
            Block,
            PageHeading,
            PageSection
        }
    })
    export default class LoginPage extends Mixins(
        UserMixin
    ) {
        get resetPasswordUrl() {
            return urls.user.resetPassword;
        }

        get registerUrl() {
            return urls.user.add;
        }

        get ready() {
            return this.$_userReady;
        }

        redirectIfSignedIn() {
            if (this.$_userReady && this.$_isSignedIn) {
                this.$router.push(urls.home);
            }
        }

        init() {
            this.$_requireUser();
        }

        mounted() {
            this.init();
            this.redirectIfSignedIn();
        }

        @Watch('$_userReady')
        userReadyChanged() {
            this.redirectIfSignedIn();
        }
    }
</script>
