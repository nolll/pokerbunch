<template>
    <span :class="cssClasses">{{formattedValue}}</span>
</template>

<script lang="ts">
    import { Component, Mixins, Prop, Vue } from 'vue-property-decorator';
    import { CssClasses } from '@/models/CssClasses';
    import { BunchMixin, FormatMixin } from '@/mixins';

    @Component
    export default class WinningsText extends Mixins(
        BunchMixin,
        FormatMixin
    ) {
        @Prop() readonly value!: number;
        @Prop({default: true}) readonly showCurrency!: boolean;

        get formattedValue() {
            if(this.showCurrency)
                return this.$_formatResult(this.value);
            return this.$_formatResultWithoutCurrency(this.value);
        }
            
        get cssClasses(): CssClasses {
            return {
                'pos-result': this.value > 0,
                'neg-result': this.value < 0
            };
        }
    }
</script>
