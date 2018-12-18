<template>
    <definition-data>{{name}}: {{formattedTime}}</definition-data>
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
        props: ['name', 'minutes'],
        computed: {
            formattedTime() {
                return this.formatTime(this.minutes);
            }
        }
    };
</script>

<style>

</style>
