<template>
    <Layout :ready="ready">
        <PageSection>
            <Block>
                <PageHeading text="Admin Tools" />
            </Block>

            <template v-if="isAdmin">
                <Block>
                    <h2 class="module-heading">Clear cache</h2>
                    <p>
                        <CustomButton text="Clear" type="action" v-on:click="clearCache" />
                    </p>
                    <p v-if="hasCacheMessage">
                        {{cacheMessage}}
                    </p>
                </Block>

                <Block>
                    <h2 class="module-heading">Send test email</h2>
                    <p>
                        <CustomButton text="Send" type="action" v-on:click="sendEmail" />
                    </p>
                    <p v-if="hasEmailMessage">
                        {{emailMessage}}
                    </p>
                </Block>
            </template>

            <Block v-else>
                <p>
                    Access denied
                </p>
            </Block>

        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { UserMixin } from '@/mixins';
    import Layout from '@/components/Layouts/Layout.vue';
    import Block from '@/components/Common/Block.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import urls from '@/urls';
    import api from '@/api'; 
    import CustomButton from '@/components/Common/CustomButton.vue';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';

    @Component({
        components: {
            Layout,
            Block,
            PageHeading,
            PageSection,
            CustomButton
        }
    })
    export default class AdminToolsPage extends Mixins(
        UserMixin
    ) {
        cacheMessage: string | null = null;
        emailMessage: string | null = null;

        get isAdmin() {
            return this.$_isAdmin;
        }

        get hasCacheMessage(){
            return !!this.cacheMessage;
        }

        get hasEmailMessage(){
            return !!this.emailMessage;
        }

        get ready() {
            return this.$_userReady;
        }

        async clearCache(){
            var response = await api.clearCache();
            this.cacheMessage = response.data.message;
            setTimeout(this.hideCacheMessage, 3000);
        }

        async sendEmail(){
            var response = await api.sendEmail();
            this.emailMessage = response.data.message;
            setTimeout(this.hideEmailMessage, 3000);
        }

        hideCacheMessage(){
            this.cacheMessage = null;
        }

        hideEmailMessage(){
            this.emailMessage = null;
        }

        init() {
            this.$_loadCurrentUser();
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
