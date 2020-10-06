<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <Block>
                <PageHeading text="Add Location" />
            </Block>

            <Block>
                <div class="field">
                    <label class="label" for="location-name">Name</label>
                    <input class="textfield" v-model="locationName" ref="locationName" id="location-name" type="text" pattern="[0-9]*">
                </div>
                <div class="buttons">
                    <CustomButton v-on:click="add" type="action" text="Add" />
                    <CustomButton v-on:click="cancel" text="Cancel" />
                </div>
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Prop, Watch } from 'vue-property-decorator';
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
    export default class AddLocationPage extends Mixins(
        BunchMixin,
        LocationMixin,
        UserMixin
    ) {
        locationName = '';

        init() {
            this.$_requireUser();
            this.$_loadBunch();
            this.$_loadLocations();
        }

        add(){
            if(this.locationName.length > 0){
                this.$_addLocation(this.locationName);
                this.redirect();
            }
        }

        cancel(){
            this.redirect();
        }

        redirect() {
            this.$router.push(urls.location.list(this.$_slug));
        }

        get ready() {
            return this.$_bunchReady && this.$_locationsReady;
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
