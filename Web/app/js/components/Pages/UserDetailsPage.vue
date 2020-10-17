<template>
    <Layout :ready="ready">
        <PageSection>
            <Block>
                <PageHeading :text="userName" />
            </Block>

            <template slot="aside1">
                <Block>
                    <img :src="avatarUrl" alt="User avatar">
                </Block>
            </template>

            <Block v-if="isEditing">
                <p>
                    <label class="label" for="display-name">Display Name</label>
                    <input class="textfield" v-model="displayName" id="display-name" type="text">
                </p>
                <p>
                    <label class="label" for="real-name">Real Name</label>
                    <input class="textfield" v-model="realName" id="real-name" type="text">
                </p>
                <p>
                    <label class="label" for="email">Email</label>
                    <input class="textfield" v-model="email" id="email" type="text">
                </p>
                <p v-if="hasError" class="validation-error">
                    {{errorMessage}}
                </p>
                <p>
                    <CustomButton v-on:click="save" type="action" text="Save" />
                    <CustomButton v-on:click="cancel" text="Cancel" />
                </p>
            </Block>
            <Block v-else>
                <ValueList>
                    <ValueListKey>Display Name</ValueListKey>
                    <ValueListValue>{{displayName}}</ValueListValue>
                    <template v-if="canEdit">
                        <ValueListKey>Real Name</ValueListKey>
                        <ValueListValue>{{realName}}</ValueListValue>
                        <ValueListKey>Email</ValueListKey>
                        <ValueListValue>{{email}}</ValueListValue>
                    </template>
                </ValueList>
            </Block>

            <Block v-if="canEdit && !isEditing">
                <CustomButton type="action" v-on:click="edit" text="Edit" />
                <CustomButton type="action" url="/user/changepassword" text="Change Password" v-if="canChangePassword" />
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
    import ValueList from '@/components/Common/ValueList/ValueList.vue';
    import ValueListKey from '@/components/Common/ValueList/ValueListKey.vue';
    import ValueListValue from '@/components/Common/ValueList/ValueListValue.vue';
    import { AxiosError } from 'axios';
    import { ApiError} from '@/models/ApiError';
    
    @Component({
        components: {
            Layout,
            Block,
            PageHeading,
            PageSection,
            CustomButton,
            ValueList,
            ValueListKey,
            ValueListValue
        }
    })
    export default class UserDetailsPage extends Mixins(
        UserMixin
    ) {
        user: User | null = null;
        userReady = false;
        displayName = '';
        realName = '';
        email = '';
        isEditing = false;
        errorMessage = '';

        get canEdit() {
            return this.$_isAdmin || this.isCurrentUser;
        }

        get isCurrentUser(){
            return this.$_userName == this.user?.userName;
        }

        get canChangePassword(){
            return this.isCurrentUser;
        }

        get ready() {
            return this.$_userReady && this.userReady;
        }

        get userName(){
            return this.user?.userName;
        }

        get avatarUrl(){
            return this.user?.avatar;
        }

        get hasError(){
            return !!this.errorMessage;
        }

        async loadUser(){
            const response = await api.getUser(this.$route.params.userName);
            this.user = response.data;
            if(this.user){
                this.setMembers(this.user);
            }
            this.userReady = true;
        }

        async save(){
            this.errorMessage = '';

            if(!this.user){
                this.isEditing = false;
                return;
            }

            this.user.displayName = this.displayName;
            this.user.realName = this.realName;
            this.user.email = this.email;
            try{
                const response = await api.updateUser(this.user);
                this.isEditing = false;
            } catch (err){
                const error = err as AxiosError<ApiError>;
                const message = error.response?.data.message || 'Unknown Error';
                this.errorMessage = message;
            }
        }

        cancel(){
            if(this.user)
                this.setMembers(this.user);
            this.isEditing = false;
        }

        setMembers(user: User){
            this.displayName = user.displayName;
            this.realName = user.realName || '';
            this.email = user.email || '';
        }

        edit(){
            this.isEditing = true;
        }

        init() {
            this.$_requireUser();
            this.loadUser();
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
