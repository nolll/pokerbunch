<template>
    <nav class="user-nav" v-if="userReady">
        <h2>Account</h2>
        <ul v-if="isSignedIn">
            <li><a :href="userDetailsUrl"><span>Signed in as {{displayName}}</span></a></li>
            <li><a href="/auth/logout"><span>Sign Out</span></a></li>
        </ul>
        <ul v-else>
            <li><a href="/auth/login"><span>Sign in</span></a></li>
            <li><a href="/user/add"><span>Register</span></a></li>
            <li><a href="/user/forgotpassword"><span>Forgot password</span></a></li>
        </ul>
    </nav>
</template>

<script>
    import { mapState } from 'vuex';

    export default {
        computed: {
            ...mapState('user', ['isSignedIn', 'userName', 'displayName', 'userReady']),
            userDetailsUrl() {
                return '/user/details/' + this.userName;
            }
        }
    };
</script>

<style>
</style>
