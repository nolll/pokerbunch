<template>
    <definition-data>{{name}}: {{formattedAmount}}</definition-data>
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
                return this.$_formatCurrency(this.amount);
            }
        }
    };
</script>

<style>

</style>
