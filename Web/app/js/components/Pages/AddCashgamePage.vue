<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <Block>
                <PageHeading text="Start Cashgame" />
            </Block>

            <Block>
                <div class="field">
                    <label class="label" for="locationId">Location</label>
                    <LocationDropdown v-model="locationId" />
                </div>
                <div class="buttons">
                    <CustomButton v-on:click="add" type="action" text="Start" />
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
    import Block from '@/components/Common/Block.vue';
    import CustomButton from '@/components/Common/CustomButton.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import { ApiParamsAddCashgame } from '@/models/ApiParamsAddCashgame';
    import urls from '@/urls';
    import { DetailedCashgameResponse } from '@/response/DetailedCashgameResponse';
    import api from '@/api';
    import { AxiosError } from 'axios';
    import { ApiError } from '@/models/ApiError';
    import LocationDropdown from '@/components/LocationDropdown.vue';
    
    @Component({
        components: {
            Layout,
            BunchNavigation,
            CustomButton,
            Block,
            PageHeading,
            PageSection,
            LocationDropdown
        }
    })
    export default class AddLocationPage extends Mixins(
        BunchMixin,
        LocationMixin,
        UserMixin
    ) {
        locationId = '';
        errorMessage = '';

        init() {
            this.$_requireUser();
            this.$_loadBunch();
            this.$_loadLocations();
        }

        async add(){
            this.errorMessage = '';

            try{
                const params: ApiParamsAddCashgame = {
                    locationId: this.locationId
                };

                const response = await api.addCashgame(this.$_slug, params);
                this.redirectToGame(response.data.id);
            } catch (err){
                const error = err as AxiosError<ApiError>;
                const message = error.response?.data.message || 'Unknown Error';
                this.errorMessage = message;
            }
        }

        cancel(){
            window.history.back();
        }

        redirectToGame(id: string) {
            this.$router.push(urls.cashgame.details(this.$_slug, id));
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
