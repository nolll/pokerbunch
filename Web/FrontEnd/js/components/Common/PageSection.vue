<template>
    <div :class="cssClasses">
        <div v-if="isAside1Enabled" :class="asideCssClasses">
            <slot name="aside1"></slot>
        </div>
        <div :class="mainCssClasses">
            <slot></slot>
        </div>
        <div v-if="isAside2Enabled" :class="asideCssClasses">
            <slot name="aside2"></slot>
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
            isAside1Enabled() {
                return this.isSlotEnabled('aside1');
            },
            isAside2Enabled() {
                return this.isSlotEnabled('aside2');
            },
            hasAside() {
                return this.isAside1Enabled || this.isAside2Enabled;
            },
            cssClasses() {
                return {
                    'page-section': true,
                    'page-section--wide': this.isWide
                }
            },
            asideCssClasses() {
                return {
                    region: true,
                    aside: true
                };
            },
            mainCssClasses() {
                return {
                    region: true,
                    width2: this.hasAside
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
