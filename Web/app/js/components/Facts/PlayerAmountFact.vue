<template>
    <DefinitionData>{{name}}: <CurrencyText :value="amount" /></DefinitionData>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import DefinitionData from '@/components/DefinitionList/DefinitionData.vue';
    import CurrencyText from '@/components/Common/CurrencyText.vue';

    @Component({
        components: {
            CurrencyText,
            DefinitionData
        }
    })
    export default class PlayerAmountFact extends Vue {
        @Prop() readonly name!: string;
        @Prop() readonly amount!: number;
    };
</script>
