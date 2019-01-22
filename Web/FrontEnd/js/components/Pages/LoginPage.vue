<template>
    <two-column :ready="true">
        <template slot="main">
            <div class="block gutter">
                <page-heading text="Sign in" />
            </div>
            <div class="block gutter">

                <p>
                    Please sign in using your username and password. <a :href="forgotPasswordUrl">Forgot password?</a>
                </p>
                <p>
                    If you are a new user, please <a :href="registerUrl">register</a>!
                </p>

                <login-form />
            </div>
        </template>
    </two-column>
</template>

<script>
    import { TwoColumn } from '@/components/Layouts';
    import { LoginForm } from '@/components';
    import { PageHeading } from '@/components/Common';

    export default {
        components: {
            TwoColumn,
            LoginForm,
            PageHeading
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
