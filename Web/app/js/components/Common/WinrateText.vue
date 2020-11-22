<template>
    <span :class="cssClasses">{{formattedValue}}</span>
</template>

<script lang="ts">
    import { Component, Mixins, Prop, Vue } from 'vue-property-decorator';
    import { CssClasses } from '@/models/CssClasses';
    import { BunchMixin, FormatMixin } from '@/mixins';

    @Component
    export default class WinrateText extends Mixins(
        BunchMixin,
        FormatMixin
    ) {
        @Prop() readonly value!: number;

        get formattedValue() {
            return this.$_formatWinrate(this.value);
        }
            
        get cssClasses(): CssClasses {
            return {
                'pos-result': this.value > 0,
                'neg-result': this.value < 0
            };
        }
    }
</script>
