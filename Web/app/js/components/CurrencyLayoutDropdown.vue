<template>
    <select :value="value" v-on:input="updateValue">
        <option value="">Select layout</option>
        <option 
            v-for="currencyLayout in currencyLayouts"
            :value="currencyLayout"
            v-bind:key="currencyLayout">
            {{getDisplayName(currencyLayout)}}
        </option>
    </select>
</template>

<script lang="ts">
    import { Component, Mixins, Prop, Vue } from 'vue-property-decorator';
    
    @Component
    export default class CurrencyLayoutDropdown extends Vue {
        @Prop() value!: string;
        @Prop({default: '$'}) readonly symbol!: string;

        get currencyLayouts(){
            return [
                '{SYMBOL} {AMOUNT}',
                '{SYMBOL}{AMOUNT}',
                '{AMOUNT}{SYMBOL}',
                '{AMOUNT} {SYMBOL}'
            ];
        }

        getDisplayName(layout: string){
            return layout.replace('{SYMBOL}', this.symbol).replace('{AMOUNT}', '123');
        }

        updateValue(event: any){
            this.$emit('input', event.target.value);
        }
    }
</script>
