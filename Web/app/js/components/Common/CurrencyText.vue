<template>
    <span>{{formattedValue}}</span>
</template>

<script lang="ts">
    import { Component, Mixins, Prop, Vue } from 'vue-property-decorator';
    import { CssClasses } from '@/models/CssClasses';
    import { BunchMixin, FormatMixin } from '@/mixins';

    @Component
    export default class CurrencyText extends Mixins(
        BunchMixin,
        FormatMixin
    ) {
        @Prop() readonly value!: number;

        get formattedValue() {
            return this.$_formatCurrency(this.value);
        }
    }
</script>
