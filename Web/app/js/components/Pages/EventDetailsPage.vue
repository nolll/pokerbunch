<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <Block>
                <PageHeading :text="name" />
            </Block>
            <Block>
                <MatrixTable :slug="slug" :games="games" />
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, EventMixin, UserMixin } from '@/mixins';
    import Layout from '@/components/Layouts/Layout.vue';
    import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
    import Block from '@/components/Common/Block.vue';
    import CustomButton from '@/components/Common/CustomButton.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import urls from '@/urls';
    import api from '@/api';
    import { ArchiveCashgameResponse } from '@/response/ArchiveCashgameResponse';
    import MatrixTable from '@/components/Matrix/MatrixTable.vue';
    
    @Component({
        components: {
            Layout,
            BunchNavigation,
            CustomButton,
            Block,
            PageHeading,
            PageSection,
            MatrixTable
        }
    })
    export default class EventDetailsPage extends Mixins(
        BunchMixin,
        EventMixin,
        UserMixin
    ) {
        games: ArchiveCashgameResponse[] = [];

        get name() {
            if(this.event)
                return this.event.name;
            return '';
        }

        get slug(){
            return this.$_slug;
        }

        get event() {
            for(let i = 0; i < this.$_events.length; i++){
                const event = this.$_events[i];
                if(event.id.toString() === this.eventId)
                    return event;
            }
            return null;
        }

        get eventId() {
            return this.$route.params.id;
        }

        get ready() {
            return this.$_bunchReady && this.$_eventsReady;
        }

        init() {
            this.$_requireUser();
            this.$_loadBunch();
            this.$_loadEvents();
            this.loadGames();
        }

        async loadGames(){
            try{
                const response = await api.getEventGames(this.$_slug, this.eventId);
                this.games = response.data;
            } catch {
                this.games = [];
            }
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
