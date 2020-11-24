<template>
    <Layout :ready="ready">
        <PageSection>
            <Block>
                <PageHeading text="Join Bunch" />
            </Block>

            <Block>
                <p>
                    Please enter your invitation code below to join the bunch {{bunchName}}
                </p>
            </Block>
            <Block v-if="errorMessage">
                <p class="validation-error">
                    {{errorMessage}}
                </p>
            </Block>
            <Block>
                <div class="field">
                    <label class="label" for="invitationCode">Invitation Code</label>
                    <input class="longfield" v-model="code" id="invitationCode" type="text">
                </div>
                <div class="buttons">
                    <CustomButton @click="joinClicked" type="action" text="Join" />
                    <CustomButton @click="cancel" text="Cancel" />
                </div>
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, PlayerMixin, UserMixin } from '@/mixins';
    import Layout from '@/components/Layouts/Layout.vue';
    import Block from '@/components/Common/Block.vue';
    import CustomButton from '@/components/Common/CustomButton.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import urls from '@/urls';
    import api from '@/api';
    import { ApiError } from '@/models/ApiError';
    import { AxiosError } from 'axios';
    
    @Component({
        components: {
            Layout,
            CustomButton,
            Block,
            PageHeading,
            PageSection
        }
    })
    export default class JoinBunchPage extends Mixins(
        BunchMixin,
        PlayerMixin,
        UserMixin
    ) {
        code = '';
        errorMessage: string | null = null;

        get routeCode(): string | undefined{
            return this.$route.params.code;
        }

        get bunchName(){
            return this.$_bunchName;
        }

        get ready() {
            return this.$_bunchReady;
        }

        joinClicked(){
            this.join(this.$_slug, this.code);
        }

        async join(bunchId: string, code: string){
            if(code.length > 0){
                try{
                    await api.joinBunch(bunchId, { code });
                    this.$_loadPlayers();
                    this.$router.push(urls.bunch.details(this.$_slug));
                } catch (err){
                    const error = err as AxiosError<ApiError>;
                    this.errorMessage = error.response?.data.message || 'Unknown Error';
                }
            }
        }

        cancel(){
            this.$router.push(urls.home);
        }

        init() {
            this.$_requireUser();
            this.$_loadBunch();
        }

        mounted() {
            this.init();
        }

        @Watch('ready')
        readyChanged() {
            if(this.ready && this.routeCode)
                this.join(this.$_slug, this.routeCode)
        }

        @Watch('$route')
        routeChanged() {
            this.init();
        }
    }
</script>
