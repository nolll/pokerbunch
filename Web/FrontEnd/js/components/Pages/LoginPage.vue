<template>
    <layout :ready="true">
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
        computed: {
            forgotPasswordUrl() {
                return urls.user.forgotPassword;
            },
            registerUrl() {
                return urls.user.add;
            }
        }
    };
</script>

<style>
</style>
