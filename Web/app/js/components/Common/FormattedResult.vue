<template>
    <span :class="cssClasses">{{text}}</span>
</template>

<script lang="ts">
    import { CssClasses } from '@/models/CssClasses';
    import { Component, Prop, Vue } from 'vue-property-decorator';

    @Component
    export default class FormattedResult extends Vue {
        @Prop() readonly text!: string;
        @Prop() readonly value!: number;

        get cssClasses(): CssClasses {
            return {
                'pos-result': this.value > 0,
                'neg-result': this.value < 0
            }
        }
    }
</script>
