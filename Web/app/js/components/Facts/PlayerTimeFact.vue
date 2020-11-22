<template>
    <DefinitionData>{{name}}: <DurationText :value="minutes" /></DefinitionData>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import DefinitionData from '@/components/DefinitionList/DefinitionData.vue';
    import DurationText from '@/components/Common/DurationText.vue';

    @Component({
        components: {
            DefinitionData,
            DurationText
        }
    })
    export default class PlayerTimeFact extends Vue {
        @Prop() readonly name!: string;
        @Prop() readonly minutes!: number;
    };
</script>
