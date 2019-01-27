<template>
    <nav class="user-nav" v-if="userReady">
        <h2>Account</h2>
        <ul v-if="isSignedIn">
            <li><custom-link :url="userDetailsUrl"><span>Signed in as {{displayName}}</span></custom-link></li>
            <li><custom-link :url="logoutUrl"><span>Sign Out</span></custom-link></li>
        </ul>
        <ul v-else>
            <li><custom-link :url="loginUrl"><span>Sign in</span></custom-link></li>
            <li><custom-link :url="registerUrl"><span>Register</span></custom-link></li>
            <li><custom-link :url="forgotPasswordUrl"><span>Forgot password</span></custom-link></li>
        </ul>
    </nav>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { USER } from '@/store-names';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';

    export default {
        components: {
            CustomLink
        },
        computed: {
            ...mapGetters(USER, [
                'isSignedIn',
                'userName',
                'displayName',
                'userReady'
            ]),
            userDetailsUrl() {
                return urls.user.details(this.userName);
            },
            registerUrl() {
                return urls.user.add;
            },
            forgotPasswordUrl() {
                return urls.user.forgotPassword;
            },
            loginUrl() {
                return urls.auth.login;
            },
            logoutUrl() {
                return urls.auth.logout;
            }
        }
    };
</script>

<style>
</style>
