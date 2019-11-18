<template>
    <definition-data>{{formattedAmount}}</definition-data>
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
        props: ['amount'],
        computed: {
            formattedAmount() {
                return this.amount;
            }
        }
    };
</script>

<style>

</style>
