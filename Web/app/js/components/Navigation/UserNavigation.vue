<template>
    <nav class="user-nav" v-if="$_userReady">
        <h2>Account</h2>
        <ul v-if="$_isSignedIn">
            <li><CustomLink :url="userDetailsUrl"><span>Signed in as {{$_displayName}}</span></CustomLink></li>
            <li><a href="#" @click.prevent="logOut"><span>Sign Out</span></a></li>
        </ul>
        <ul v-else>
            <li><CustomLink :url="loginUrl"><span>Sign in</span></CustomLink></li>
            <li><CustomLink :url="registerUrl"><span>Register</span></CustomLink></li>
            <li><CustomLink :url="resetPasswordUrl"><span>Reset password</span></CustomLink></li>
        </ul>
    </nav>
</template>

<script lang="ts">
    import { Component, Mixins, Prop, Vue } from 'vue-property-decorator';
    import { UserMixin } from '@/mixins';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';
    import auth from '@/auth';
    import api from '@/api';

    @Component({
        components: {
            CustomLink
        }
    })
    export default class UserNavigation extends Mixins(UserMixin) {
        async logOut() {
            auth.clearToken();
            await api.signOut();
            this.redirectHome();
        }

        get userDetailsUrl() {
            return urls.user.details(this.$_userName);
        }

        get registerUrl() {
            return urls.user.add;
        }

        get resetPasswordUrl() {
            return urls.user.resetPassword;
        }

        get loginUrl() {
            return urls.auth.login;
        }
        
        redirectHome() {
            window.location.href = urls.home;
        }
    }
</script>
