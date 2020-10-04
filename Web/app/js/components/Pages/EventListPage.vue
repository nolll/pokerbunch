<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <template slot="aside1">
                <Block>
                    <CustomButton :url="addEventUrl" type="action" text="Add event" />
                </Block>
            </template>

            <Block>
                <PageHeading text="Events" />
            </Block>

            <Block>
                <EventList />
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, EventMixin, UserMixin } from '@/mixins';
    import Layout from '@/components/Layouts/Layout.vue';
    import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
    import EventList from '@/components/EventList/EventList.vue';
    import Block from '@/components/Common/Block.vue';
    import CustomButton from '@/components/Common/CustomButton.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import urls from '@/urls';
    
    @Component({
        components: {
            Layout,
            BunchNavigation,
            EventList,
            CustomButton,
            Block,
            PageHeading,
            PageSection
        }
    })
    export default class EventListPage extends Mixins(
        BunchMixin,
        EventMixin,
        UserMixin
    ) {

        get addEventUrl() {
            return urls.event.add(this.$_slug);
        }

        get ready() {
            return this.$_bunchReady && this.$_eventsReady;
        }

        init() {
            this.$_requireUser();
            this.$_loadBunch();
            this.$_loadEvents();
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
