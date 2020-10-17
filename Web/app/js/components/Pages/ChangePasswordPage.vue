<template>
    <Layout :ready="ready">
        <PageSection>
            <Block>
                <PageHeading text="Change Password" />
            </Block>

            <template v-if="isSaving">
                <Block>
                    <p>
                        Your password was changed
                    </p>
                </Block>
                <Block>
                    <CustomButton type="action" v-on:click="back" text="Back" />
                </Block>
            </template>
            <Block v-else>
                <p>
                    <label class="label" for="oldPassword">Old Password</label>
                    <input class="textfield" v-model="oldPassword" id="oldPassword" type="password">
                </p>
                <p>
                    <label class="label" for="newPassword">New Password</label>
                    <input class="textfield" v-model="newPassword" id="newPassword" type="password">
                </p>
                <p>
                    <label class="label" for="repeat">Repeat New Password</label>
                    <input class="textfield" v-model="repeat" id="repeat" type="password">
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
    import { User } from '@/models/User';
    import { AxiosError } from 'axios';
    import { ApiError} from '@/models/ApiError';
    import { ApiParamsChangePassword } from '@/models/ApiParamsChangePassword';
    
    @Component({
        components: {
            Layout,
            Block,
            PageHeading,
            PageSection,
            CustomButton
        }
    })
    export default class ChangePasswordPage extends Mixins(
        UserMixin
    ) {
        oldPassword = '';
        newPassword = '';
        repeat = '';
        errorMessage = '';
        isSaving = false;

        get ready() {
            return this.$_userReady;
        }

        get hasError(){
            return !!this.errorMessage;
        }

        async save(){
            this.errorMessage = '';

            if(this.repeat !== this.newPassword){
                this.errorMessage = 'Passwords doesn\'t match';
                return;
            }

            try{
                const params: ApiParamsChangePassword = {
                    oldPassword: this.oldPassword,
                    newPassword: this.newPassword
                };
                const response = await api.changePassword(params);
                this.isSaving = true;
            } catch (err){
                const error = err as AxiosError<ApiError>;
                const message = error.response?.data.message || 'Unknown Error';
                this.errorMessage = message;
            }
        }

        back(){
            this.$router.push(urls.user.details(this.$_userName));
        }

        init() {
            this.$_requireUser();
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
