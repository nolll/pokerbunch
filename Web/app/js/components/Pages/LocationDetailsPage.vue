<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <Block>
                <PageHeading :text="name" />
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
    export default class LocationDetailsPage extends Mixins(
        BunchMixin,
        LocationMixin,
        UserMixin
    ) {

        get name() {
            if(this.location)
                return this.location.name;
            return '';
        }

        get location() {
            for(let i = 0; i < this.$_locations.length; i++){
                const location = this.$_locations[i];
                if(location.id.toString() === this.locationId)
                    return location;
            }
            return null;
        }

        get locationId() {
            return this.$route.params.id;
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
