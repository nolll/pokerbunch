<template>
    <definition-data>{{formattedTime}}</definition-data>
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
        props: ['minutes'],
        computed: {
            formattedTime() {
                return this.formatTime(this.minutes);
            }
        }
    };
</script>

<style>

</style>
