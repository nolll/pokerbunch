<template>
    <Layout :ready="ready">
        <PageSection>
            <Block>
                <PageHeading text="Admin Tools" />
            </Block>

            <template v-if="isAdmin">
                <Block>
                    <ClearCache />
                </Block>

                <Block>
                    <SendEmail />
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
    import SendEmail from '@/components/Admin/SendEmail.vue';
    import ClearCache from '@/components/Admin/ClearCache.vue';

    @Component({
        components: {
            Layout,
            Block,
            PageHeading,
            PageSection,
            SendEmail,
            ClearCache
        }
    })
    export default class AdminToolsPage extends Mixins(
        UserMixin
    ) {
        get isAdmin() {
            return this.$_isAdmin;
        }

        get ready() {
            return this.$_userReady;
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
