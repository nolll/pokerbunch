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

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { CssClasses } from '@/models/CssClasses';

    @Component
    export default class PageSection extends Vue {
        @Prop({default: false}) readonly isWide!: boolean;

        get isAside1Enabled() {
            return this.isSlotEnabled('aside1');
        }

        get isAside2Enabled() {
            return this.isSlotEnabled('aside2');
        }
        
        get hasAside() {
            return this.isAside1Enabled || this.isAside2Enabled;
        }
        
        get cssClasses(): CssClasses {
            return {
                'page-section': true,
                'page-section--wide': this.isWide
            }
        }
        
        get asideCssClasses(): CssClasses {
            return {
                region: true,
                aside: true
            };
        }
        
        get mainCssClasses(): CssClasses {
            return {
                region: true,
                width2: this.hasAside
            };
        }

        isSlotEnabled(name: string) {
            return !!this.$slots[name];
        }
    }
</script>
