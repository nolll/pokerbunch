<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <Block>
                <PageHeading :text="$_bunchName" />
            </Block>

            <Block v-if="hasDescription">
                {{$_description}}
            </Block>

            <Block v-if="hasHouseRules">
                <h2>House Rules</h2>
                <p>
                    {{$_houseRules}}
                </p>
            </Block>

            <Block v-if="$_isManager">
                <CustomLink :url="editUrl">Edit Bunch</CustomLink>
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, UserMixin } from '@/mixins';
    import urls from '@/urls';
    import Layout from '@/components/Layouts/Layout.vue';
    import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
    import Block from '@/components/Common/Block.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    
    @Component({
        components: {
            Layout,
            BunchNavigation,
            Block,
            PageHeading,
            PageSection,
            CustomLink
        }
    })
    export default class BunchDetailsPage extends Mixins(
        BunchMixin,
        UserMixin
    ) {
        get hasDescription() {
            return !!this.$_description;
        }

        get hasHouseRules() {
            return !!this.$_houseRules;
        }

        get canEdit() {
            return this.$_isManager;
        }

        get editUrl() {
            return urls.bunch.edit(this.$_slug);
        }
        
        get ready() {
            return this.$_bunchReady;
        }

        init() {
            this.$_requireUser();
            this.$_loadBunch();
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
