<template>
    <two-column :ready="true">
        <template slot="main">
            <page-section>
                <page-heading text="Sign in" />
            </page-section>
            <page-section>
                <p>
                    Please sign in using your username and password. <custom-link :url="forgotPasswordUrl">Forgot password?</custom-link>
                </p>
                <p>
                    If you are a new user, please <custom-link :url="registerUrl">register</custom-link>!
                </p>
                <login-form />
            </page-section>
        </template>
    </two-column>
</template>

<script>
    import { TwoColumn } from '@/components/Layouts';
    import { LoginForm } from '@/components';
    import { CustomLink, PageHeading, PageSection } from '@/components/Common';
    import urls from '@/urls';

    export default {
        components: {
            TwoColumn,
            LoginForm,
            CustomLink,
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
