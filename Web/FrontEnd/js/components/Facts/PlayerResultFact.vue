<template>
    <definition-data>{{name}}: <span :class="cssClass">{{formattedAmount}}</span></definition-data>
</template>

<script>
    import { FormatMixin } from '@/mixins'
    import { DefinitionData } from "../DefinitionList";

    export default {
        mixins: [
            FormatMixin
        ],
        components: {
            DefinitionData
        },
        props: ['name', 'amount'],
        computed: {
            formattedAmount() {
                return this.formatResult(this.amount);
            },
            cssClass() {
                if (this.amount === 0)
                    return '';
                return this.amount > 0 ? 'pos-result' : 'neg-result';
            }
        }
    };
</script>

<style>

</style>
