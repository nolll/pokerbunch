<template>
    <div>
        <page-section>
            <div class="page-header">
                <div class="logo"><custom-link :url="homeUrl" cssClasses="logo-link">Poker Bunch</custom-link></div>
                <div v-if="isTopNavEnabled">
                    <slot name="top-nav"></slot>
                </div>
            </div>
        </page-section>

        <div v-if="ready">
            <div class="main clearfix">
                <slot></slot>
            </div>

            <page-section>
                <slot name="bottom-nav"><user-navigation /></slot>
            </page-section>
        </div>

        <div v-else>
            <spinner />
        </div>
    </div>
</template>

<script>
    import { UserNavigation } from '@/components/Navigation';
    import { PageSection, Spinner } from '@/components/Common';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';

    export default {
        components: {
            UserNavigation,
            PageSection,
            Spinner,
            CustomLink
        },
        props: {
            ready: {
                type: Boolean
            }
        },
        computed: {
            isTopNavEnabled() {
                return this.isSlotEnabled('top-nav');
            },
            homeUrl() {
                return urls.home;
            }
        },
        methods: {
            isSlotEnabled(name) {
                return !!this.$slots[name];
            }
        }
    };
</script>

<style>
</style>
