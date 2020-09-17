<template>
    <definition-data>{{name}}: <span :class="cssClasses">{{formattedAmount}}</span></definition-data>
</template>

<script>
    import { FormatMixin } from '@/mixins'
    import { DefinitionData } from '@/components/DefinitionList';

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
                return this.$_formatResult(this.amount);
            },
            cssClasses() {
                return {
                    'pos-result': this.amount > 0,
                    'neg-result': this.amount < 0
                };
            }
        }
    };
</script>

<style>

</style>
