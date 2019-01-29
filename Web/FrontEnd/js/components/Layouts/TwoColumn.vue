<template>
    <div>
        <header class="page-section">
            <div class="page-header">
                <div class="logo"><custom-link :url="homeUrl" cssClasses="logo-link">Poker Bunch</custom-link></div>
                <div v-if="isTopNavEnabled">
                    <slot name="top-nav"></slot>
                </div>
            </div>
        </header>

        <div v-if="ready">
            <div class="main clearfix">
                <div class="page-section">
                    <div v-if="isContentNavEnabled" class="heading-nav-container block gutter">
                        <slot name="content-nav"></slot>
                    </div>
                    <div v-if="isAsideEnabled" class="region width1 aside1">
                        <slot name="aside"></slot>
                    </div>
                    <div v-if="isMainEnabled" :class="mainCssClasses">
                        <slot name="main"></slot>
                    </div>
                    <div v-if="isMainWideEnabled" class="region width3">
                        <slot name="main-wide"></slot>
                    </div>
                </div>
            </div>

            <div class="page-section">
                <slot name="bottom-nav"><user-navigation /></slot>
            </div>
        </div>

        <div v-else>
            <spinner />
        </div>
    </div>
</template>

<script>
    import { UserNavigation } from '@/components/Navigation';
    import { Spinner } from '@/components/Common';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';

    export default {
        components: {
            UserNavigation,
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
            isContentNavEnabled() {
                return this.isSlotEnabled('content-nav');
            },
            isAsideEnabled() {
                return this.isSlotEnabled('aside');
            },
            isMainEnabled() {
                return this.isSlotEnabled('main');
            },
            isMainWideEnabled() {
                return this.isSlotEnabled('main-wide');
            },
            mainCssClasses() {
                return {
                    region: true,
                    width2: this.isAsideEnabled,
                    width3: !this.isAsideEnabled
                };
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
