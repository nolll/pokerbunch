<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <template slot="aside1">
                <Block>
                    <CustomButton :url="addLocationUrl" type="action" text="Add location" />
                </Block>
            </template>

            <Block>
                <PageHeading text="Locations" />
            </Block>

            <Block>
                <LocationList />
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, LocationMixin, UserMixin } from '@/mixins';
    import Layout from '@/components/Layouts/Layout.vue';
    import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
    import LocationList from '@/components/LocationList/LocationList.vue';
    import Block from '@/components/Common/Block.vue';
    import CustomButton from '@/components/Common/CustomButton.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import urls from '@/urls';
    
    @Component({
        components: {
            Layout,
            BunchNavigation,
            LocationList,
            CustomButton,
            Block,
            PageHeading,
            PageSection
        }
    })
    export default class LocationListPage extends Mixins(
        BunchMixin,
        LocationMixin,
        UserMixin
    ) {

        get addLocationUrl() {
            return urls.location.add(this.$_slug);
        }

        get ready() {
            return this.$_bunchReady && this.$_locationsReady;
        }

        init() {
            this.$_requireUser();
            this.$_loadBunch();
            this.$_loadLocations();
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
