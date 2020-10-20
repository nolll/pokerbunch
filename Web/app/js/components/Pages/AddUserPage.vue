<template>
    <Layout :ready="true">
        <PageSection>
            <Block>
                <PageHeading text="Register" />
            </Block>

            <template v-if="isSaving">
                <Block>
                    <p>
                        Welcome to Poker Bunch!
                    </p>
                    <p>
                        <CustomLink :url="loginUrl">Sign in here!</CustomLink> GL!
                    </p>
                </Block>
            </template>
            <Block v-else>
                <p>
                    <label class="label" for="userName">Login Name</label>
                    <input class="textfield" v-model="userName" id="userName" type="text">
                </p>
                <p>
                    <label class="label" for="displayName">Display Name</label>
                    <input class="textfield" v-model="displayName" id="displayName" type="text">
                </p>
                <p>
                    <label class="label" for="email">Email</label>
                    <input class="textfield" v-model="email" id="email" type="email">
                </p>
                <p>
                    <label class="label" for="paddword">Password</label>
                    <input class="textfield" v-model="password" id="password" type="password">
                </p>
                <p>
                    <label class="label" for="repeatPassword">Repeat Password</label>
                    <input class="textfield" v-model="repeatPassword" id="repeatPassword" type="password">
                </p>
                <p v-if="hasError" class="validation-error">
                    {{errorMessage}}
                </p>
                <p>
                    <CustomButton v-on:click="save" type="action" text="Save" />
                    <CustomButton v-on:click="back" text="Cancel" />
                </p>
            </Block>

        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { UserMixin } from '@/mixins';
    import urls from '@/urls';
    import api from '@/api';
    import Layout from '@/components/Layouts/Layout.vue';
    import Block from '@/components/Common/Block.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import CustomButton from '@/components/Common/CustomButton.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import { User } from '@/models/User';
    import { AxiosError } from 'axios';
    import { ApiError} from '@/models/ApiError';
    import { ApiParamsAddUser } from '@/models/ApiParamsAddUser';
    
    @Component({
        components: {
            Layout,
            Block,
            PageHeading,
            PageSection,
            CustomButton,
            CustomLink
        }
    })
    export default class AddUserPage extends Mixins(
        UserMixin
    ) {
        userName = '';
        displayName = '';
        email = '';
        password = '';
        repeatPassword = '';
        errorMessage = '';
        isSaving = false;

        get hasError(){
            return !!this.errorMessage;
        }

        get loginUrl(){
            return urls.auth.login;
        }

        async save(){
            this.errorMessage = '';

            if(this.repeatPassword !== this.password){
                this.errorMessage = 'Passwords doesn\'t match';
                return;
            }

            try{
                const params: ApiParamsAddUser = {
                    userName: this.userName,
                    displayName: this.displayName,
                    email: this.email,
                    password: this.password
                };
                const response = await api.addUser(params);
                this.isSaving = true;
            } catch (err){
                const error = err as AxiosError<ApiError>;
                const message = error.response?.data.message || 'Unknown Error';
                this.errorMessage = message;
            }
        }

        back(){
            history.back();
        }

        init() {
        }

        mounted() {
            this.init();
        }

        @Watch('$route')
        routeChanged() {
            this.init();
        }
    }
</script>
