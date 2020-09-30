<template>
    <div>
        <PageSection>
            <div class="page-header">
                <div class="logo"><CustomLink :url="homeUrl" cssClasses="logo-link">Poker Bunch</CustomLink></div>
                <div v-if="isTopNavEnabled">
                    <slot name="top-nav"></slot>
                </div>
            </div>
        </PageSection>

        <div v-if="ready">
            <div class="main clearfix">
                <slot></slot>
            </div>

            <PageSection>
                <slot name="bottom-nav"><UserNavigation /></slot>
            </PageSection>
        </div>

        <div v-else>
            <Spinner />
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import UserNavigation from '@/components/Navigation/UserNavigation.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import Spinner from '@/components/Common/Spinner.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';

    @Component({
        components: {
            UserNavigation,
            PageSection,
            Spinner,
            CustomLink
        }
    })
    export default class Layout extends Vue {
        @Prop({default: false}) readonly ready!: boolean;

        get isTopNavEnabled() {
            return this.isSlotEnabled('top-nav');
        }
        
        get homeUrl() {
            return urls.home;
        }

        isSlotEnabled(name: string) {
            return !!this.$slots[name];
        }
    }
</script>
