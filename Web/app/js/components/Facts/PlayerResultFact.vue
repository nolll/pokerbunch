<template>
    <DefinitionData>{{name}}: <span :class="cssClasses">{{formattedAmount}}</span></DefinitionData>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import { FormatMixin } from '@/mixins'
    import DefinitionData from '@/components/DefinitionList/DefinitionData.vue';
    import { CssClasses } from '@/models/CssClasses';

    @Component({
        components: {
            DefinitionData
        }
    })
    export default class PlayerResultFact extends Mixins(
        FormatMixin
    ) {
        @Prop(String) readonly name!: string;
        @Prop(Number) readonly amount!: number;

        get formattedAmount() {
            return this.$_formatResult(this.amount);
        }

        get cssClasses(): CssClasses {
            return {
                'pos-result': this.amount > 0,
                'neg-result': this.amount < 0
            };
        }
    }
</script>
