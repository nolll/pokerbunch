<template>
    <div :class="cssClasses">
        <div v-if="isAsideEnabled" :class="asideCssClasses">
            <slot name="aside"></slot>
        </div>
        <div :class="mainCssClasses">
            <slot></slot>
        </div>
    </div>
</template>

<script>
    export default {
        props: {
            isWide: {
                type: Boolean
            }
        },
        computed: {
            isAsideEnabled() {
                return this.isSlotEnabled('aside');
            },
            cssClasses() {
                return {
                    'page-section': true,
                    gutter2: true,
                    clearfix: true,
                    wide: this.isWide
                }
            },
            asideCssClasses() {
                return {
                    region: true,
                    width1: true,
                    aside1: true
                };
            },
            mainCssClasses() {
                return {
                    region: true,
                    width2: this.isAsideEnabled,
                    width3: !this.isAsideEnabled
                };
            }
        },
        methods: {
            isSlotEnabled(name) {
                return !!this.$slots[name];
            }
        }
    };
</script>

<style lang="less">
</style>
