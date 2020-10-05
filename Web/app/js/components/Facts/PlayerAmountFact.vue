<template>
    <DefinitionData>{{name}}: {{formattedAmount}}</DefinitionData>
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
    export default class PlayerAmountFact extends Mixins(FormatMixin) {
        @Prop() readonly name!: string;
        @Prop() readonly amount!: number;
        
        get formattedAmount() {
            return this.$_formatCurrency(this.amount);
        }
    };
</script>
