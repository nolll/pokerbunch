<template>
    <Layout :ready="true">
        <PageSection>
            <Block>
                <PageHeading text="Reset Password" />
            </Block>

            <template v-if="isSaving">
                <Block>
                    <p>
                        An email was sent.
                    </p>
                </Block>
                <Block>
                    <CustomButton type="action" :url="loginUrl" text="Sign in" />
                </Block>
            </template>
            <Block v-else>
                <p>
                    Enter your email address to reset your password.
                </p>
                <p>
                    <label class="label" for="email">Email</label>
                    <input class="textfield" v-model="email" id="email" type="text">
                </p>
                <p v-if="hasError" class="validation-error">
                    {{errorMessage}}
                </p>
                <p>
                    <CustomButton v-on:click="send" type="action" text="Send" />
                    <CustomButton v-on:click="back" text="Cancel" />
                </p>
            </Block>

        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Vue, Watch } from 'vue-property-decorator';
    import { UserMixin } from '@/mixins';
    import urls from '@/urls';
    import api from '@/api';
    import Layout from '@/components/Layouts/Layout.vue';
    import Block from '@/components/Common/Block.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import CustomButton from '@/components/Common/CustomButton.vue';
    import { User } from '@/models/User';
    import { AxiosError } from 'axios';
    import { ApiError} from '@/models/ApiError';
    import { ApiParamsResetPassword } from '@/models/ApiParamsResetPassword';
    
    @Component({
        components: {
            Layout,
            Block,
            PageHeading,
            PageSection,
            CustomButton
        }
    })
    export default class ResetPasswordPage extends Vue {
        email = '';
        errorMessage = '';
        isSaving = false;

        get hasError(){
            return !!this.errorMessage;
        }

        get loginUrl(){
            return urls.auth.login;
        }

        async send(){
            this.errorMessage = '';

            try{
                const params: ApiParamsResetPassword = {
                    email: this.email
                };
                const response = await api.resetPassword(params);
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
