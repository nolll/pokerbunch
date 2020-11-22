<template>
    <span>{{formattedValue}}</span>
</template>

<script lang="ts">
    import { Component, Mixins, Prop, Vue } from 'vue-property-decorator';
    import { CssClasses } from '@/models/CssClasses';
    import { FormatMixin } from '@/mixins';

    @Component
    export default class DurationText extends Mixins(
        FormatMixin
    ) {
        @Prop() readonly value!: number;

        get formattedValue() {
            return this.$_formatDuration(this.value);
        }
    }
</script>
