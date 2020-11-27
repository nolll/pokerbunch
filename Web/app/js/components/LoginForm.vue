<template>
    <div>
        <div class="errors" v-if="isErrorVisible">
            <p class="validation-error">
                {{errorMessage}}
            </p>
        </div>
        <fieldset>
            <p>
                <label class="label" for="username">Email or User Name</label>
                <input type="text" id="username" v-model="username" ref="username" class="textfield">
            </p>
            <p>
                <label class="label" for="password">Password</label>
                <input type="password" id="password" v-model="password" class="textfield">
            </p>
            <p class="checkbox-layout">
                <label class="checkbox-label" for="rememberme">Keep me signed in</label>
                <input type="checkbox" v-model="rememberMe" id="rememberme">
            </p>
            <div class="buttons">
                <button @click="login" class="button button--action">Sign in</button>
            </div>
        </fieldset>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import querystring from '@/querystring';
    import api from '@/api';
    import auth from '@/auth'
    import { ApiParamsGetToken } from '@/models/ApiParamsGetToken';

    @Component
    export default class LoginForm extends Vue {
        @Prop() readonly text!: string;

        username = '';
        password = '';
        rememberMe = false;
        errorMessage: string | null = null;

        get isErrorVisible() {
            return this.errorMessage !== null;
        }

        async login() {
            this.clearError();

            if (this.validateForm()) {
                var data: ApiParamsGetToken = {
                    username: this.username,
                    password: this.password
                }

                try{
                    const response = await api.getToken(data);
                    this.saveToken(response.data);
                    this.redirect();
                } catch {
                    this.showError('There was something wrong with your username or password. Please try again.');
                }
            } else {
                this.showError('Please enter your username (or email) and password');
            }
        }

        validateForm() {
            this.clearError();
            if (this.username === '' || this.password === '')
                return false;
            return true;
        }

        clearError() {
            this.errorMessage = null;
        }

        showError(message: string) {
            this.errorMessage = message;
        }

        saveToken(token: string) {
            auth.setToken(token, this.rememberMe);
        }

        redirect() {
            const returnUrl = querystring.get('returnurl');
            const redirectUrl = returnUrl || '/';
            window.location.href = redirectUrl;
        }
    }
</script>
