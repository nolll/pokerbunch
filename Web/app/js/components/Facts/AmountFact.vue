<template>
    <DefinitionData>{{amount}}</DefinitionData>
</template>

<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';
    import DefinitionData from '@/components/DefinitionList/DefinitionData.vue';

    @Component({
        components: {
            DefinitionData
        }
    })
    export default class AmountFact extends Vue {
        @Prop(String) readonly amount!: string;
    };
</script>
