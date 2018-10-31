<template>
    <div>
        <header class="page-section">
            <div class="page-header">
                <div class="logo"><a href="/" class="logo-link">Poker Bunch</a></div>
                <div v-if="isTopNavEnabled">
                    <slot name="top-nav"></slot>
                </div>
            </div>
        </header>

        <div class="main clearfix">
            <div class="page-section">
                <div v-if="isContentNavEnabled" class="heading-nav-container block gutter">
                    <slot name="content-nav"></slot>
                </div>
                <div v-if="isAsideEnabled" class="region width1 aside1">
                    <slot name="aside"></slot>
                </div>
                <div :class="'region ' + mainWidthClass">
                    <slot name="main"></slot>
                </div>
            </div>
        </div>

        <div class="page-section">
            <slot name="bottom-nav"><user-navigation /></slot>
        </div>
    </div>
</template>

<script>
    import { UserNavigation } from "../Navigation";

    export default {
        components: {
            UserNavigation
        },
        computed: {
            isTopNavEnabled: function () {
                return this.isSlotEnabled('top-nav');
            },
            isContentNavEnabled: function () {
                return this.isSlotEnabled('content-nav');
            },
            isAsideEnabled: function () {
                return this.isSlotEnabled('aside');
            },
            mainWidthClass: function () {
                return 'width' + this.mainWidth;
            },
            mainWidth: function () {
                return this.isAsideEnabled ? 2 : 3;
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
