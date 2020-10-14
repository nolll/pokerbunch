<template>
    <Layout :ready="ready">
        <PageSection>
            
            <Block>
                <PageHeading text="Bunches" />
            </Block>

            <Block v-if="isAdmin">
                <BunchList />
            </Block>

            <Block v-else>
                Access denied
            </Block>

        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, UserMixin } from '@/mixins';
    import Layout from '@/components/Layouts/Layout.vue';
    import Block from '@/components/Common/Block.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import BunchList from '@/components/BunchList/BunchList.vue';

    @Component({
        components: {
            Layout,
            Block,
            PageHeading,
            PageSection,
            BunchList
        }
    })
    export default class BunchListPage extends Mixins(
        BunchMixin,
        UserMixin
    ) {
        get ready() {
            return this.$_userReady && this.$_bunchesReady;
        }

        get isAdmin(){
            return this.$_isAdmin;
        }

        init() {
            this.$_requireUser();
            this.$_loadBunches();
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
