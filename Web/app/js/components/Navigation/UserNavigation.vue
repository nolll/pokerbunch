<template>
    <nav class="user-nav" v-if="$_userReady">
        <h2>Account</h2>
        <ul v-if="$_isSignedIn">
            <li><custom-link :url="userDetailsUrl"><span>Signed in as {{$_displayName}}</span></custom-link></li>
            <li><a href="#" @click.prevent="logOut"><span>Sign Out</span></a></li>
        </ul>
        <ul v-else>
            <li><custom-link :url="loginUrl"><span>Sign in</span></custom-link></li>
            <li><custom-link :url="registerUrl"><span>Register</span></custom-link></li>
            <li><custom-link :url="forgotPasswordUrl"><span>Forgot password</span></custom-link></li>
        </ul>
    </nav>
</template>

<script>
    import { UserMixin } from '@/mixins';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';
    import auth from '@/auth';
    import api from '@/api';

    export default {
        components: {
            CustomLink
        },
        mixins: [
            UserMixin
        ],
        computed: {
            userDetailsUrl() {
                return urls.user.details(this.$_userName);
            },
            registerUrl() {
                return urls.user.add;
            },
            forgotPasswordUrl() {
                return urls.user.forgotPassword;
            },
            loginUrl() {
                return urls.auth.login;
            }
        },
        methods: {
            logOut() {
                auth.clearToken();
                api.signOut()
                    .then(() => {
                        this.redirectHome();
                    });
            },
            redirectHome() {
                window.location.href = urls.home;
            }
        }
    };
</script>

<style>
</style>
