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

<script>
    import querystring from '@/querystring';
    import api from '@/api';
    import auth from '@/auth'

    export default {
        computed: {
            isErrorVisible() {
                return this.errorMessage !== null;
            }
        },
        methods: {
            login() {
                this.clearError();
                var self = this;

                if (this.validateForm()) {
                    var data = {
                        username: this.username,
                        password: this.password,
                        rememberMe: this.rememberMe
                    }

                    api.getToken(data)
                        .then(function (response) {
                            if (response.data.success) {
                                self.saveToken(response.data.token);
                                self.redirect();
                            } else {
                                self.showError(response.data.message);
                            }
                        })
                        .catch(function () {
                            self.showError('There was something wrong with your username or password. Please try again.');
                        });
                } else {
                    this.showError('Please enter your username (or email) and password');
                }
            },
            validateForm() {
                this.clearError();
                if (this.username === '' || this.password === '')
                    return false;
                return true;
            },
            clearError() {
                this.errorMessage = null;
            },
            showError(message) {
                this.errorMessage = message;
            },
            saveToken(token) {
                auth.setToken(token, this.rememberMe);
            },
            redirect() {
                var returnUrl = querystring.get('returnurl');
                var redirectUrl = returnUrl || '/';
                window.location.href = redirectUrl;
            }
        },
        data: function () {
            return {
                username: '',
                password: '',
                rememberMe: false,
                errorMessage: null
            }
        }
    };
</script>

<style>
</style>
