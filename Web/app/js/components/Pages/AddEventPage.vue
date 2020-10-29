<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <Block>
                <PageHeading text="Add Event" />
            </Block>

            <Block>
                <div class="field">
                    <label class="label" for="event-name">Name</label>
                    <input class="textfield" v-model="eventName" ref="eventName" id="event-name" type="text">
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
    import { BunchMixin, EventMixin, UserMixin } from '@/mixins';
    import Layout from '@/components/Layouts/Layout.vue';
    import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
    import Block from '@/components/Common/Block.vue';
    import CustomButton from '@/components/Common/CustomButton.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import urls from '@/urls';
    
    @Component({
        components: {
            Layout,
            BunchNavigation,
            CustomButton,
            Block,
            PageHeading,
            PageSection
        }
    })
    export default class AddEventPage extends Mixins(
        BunchMixin,
        EventMixin,
        UserMixin
    ) {
        eventName = '';

        init() {
            this.$_requireUser();
            this.$_loadBunch();
            this.$_loadEvents();
        }

        add(){
            if(this.eventName.length > 0){
                this.$_addEvent(this.eventName);
                this.redirect();
            }
        }

        cancel(){
            this.redirect();
        }

        redirect() {
            this.$router.push(urls.event.list(this.$_slug));
        }

        get ready() {
            return this.$_bunchReady && this.$_eventsReady;
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
