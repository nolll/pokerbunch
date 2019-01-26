<template>
    <nav class="user-nav" v-if="userReady">
        <h2>Account</h2>
        <ul v-if="isSignedIn">
            <li><custom-link :url="userDetailsUrl"><span>Signed in as {{displayName}}</span></custom-link></li>
            <li><custom-link url="/auth/logout"><span>Sign Out</span></custom-link></li>
        </ul>
        <ul v-else>
            <li><custom-link url="/auth/login"><span>Sign in</span></custom-link></li>
            <li><custom-link url="/user/add"><span>Register</span></custom-link></li>
            <li><custom-link url="/user/forgotpassword"><span>Forgot password</span></custom-link></li>
        </ul>
    </nav>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { USER } from '@/store-names';
    import CustomLink from '@/components/Common/CustomLink.vue';

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
                return '/user/details/' + this.userName;
            }
        }
    };
</script>

<style>
</style>
