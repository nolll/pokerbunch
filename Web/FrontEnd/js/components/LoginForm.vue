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
                <button v-on:click="login" class="button button--action">Sign in</button>
            </div>
        </fieldset>
    </div>
</template>

<script>
    import querystring from '../querystring';
    import api from '../api';
    import validate from '../validate';
    import forms from '../forms';

    export default {
        computed: {
            isErrorVisible: function () {
                return this.errorMessage !== null;
            }
        },
        methods: {
            login: function () {
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
                                self.setCookie();
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
            validateForm: function () {
                this.clearError();
                if (this.username === '' || this.password === '')
                    return false;
                return true;
            },
            clearError: function () {
                this.errorMessage = null;
            },
            showError: function (message) {
                this.errorMessage = message;
            },
            setCookie: function () {
                // set cookie here when form auth isn't needed anymore
            },
            redirect: function () {
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
