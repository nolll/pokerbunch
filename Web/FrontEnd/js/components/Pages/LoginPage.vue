<template>
    <two-column :ready="true">
        <template slot="main">
            <page-section>
                <page-heading text="Sign in" />
            </page-section>
            <page-section>
                <p>
                    Please sign in using your username and password. <a :href="forgotPasswordUrl">Forgot password?</a>
                </p>
                <p>
                    If you are a new user, please <a :href="registerUrl">register</a>!
                </p>
                <login-form />
            </page-section>
        </template>
    </two-column>
</template>

<script>
    import { TwoColumn } from '@/components/Layouts';
    import { LoginForm } from '@/components';
    import { PageHeading, PageSection } from '@/components/Common';

    export default {
        components: {
            TwoColumn,
            LoginForm,
            PageHeading,
            PageSection
        },
        computed: {
            forgotPasswordUrl() {
                return '/user/forgotpassword';
            },
            registerUrl() {
                return '/user/add';
            }
        }
    };
</script>

<style>
</style>
