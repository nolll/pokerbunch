<template>
    <DefinitionData>{{name}}: {{formattedTime}}</DefinitionData>
</template>

<script lang="ts">
    import { Component, Mixins, Prop } from 'vue-property-decorator';
    import { FormatMixin } from '@/mixins';
    import DefinitionData from '@/components/DefinitionList/DefinitionData.vue';

    @Component({
        components: {
            DefinitionData
        }
    })
    export default class PlayerTimeFact extends Mixins(FormatMixin) {
        @Prop(String) readonly name!: string;
        @Prop(Number) readonly minutes!: number;

        get formattedTime() {
            return this.$_formatDuration(this.minutes);
        }
    };
</script>
