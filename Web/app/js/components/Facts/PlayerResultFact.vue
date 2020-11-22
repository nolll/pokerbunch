<template>
    <DefinitionData>{{name}}: <WinningsText :value="amount" /></DefinitionData>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import DefinitionData from '@/components/DefinitionList/DefinitionData.vue';
    import WinningsText from '@/components/Common/WinningsText.vue'

    @Component({
        components: {
            DefinitionData,
            WinningsText
        }
    })
    export default class PlayerResultFact extends Vue {
        @Prop() readonly name!: string;
        @Prop() readonly amount!: number;
    }
</script>
