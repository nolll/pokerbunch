<template>
    <td class="table-list__cell" :class="cssClasses">
        <slot></slot>
    </td>
</template>

<script lang="ts">
    import { CssClasses } from '@/models/CssClasses';
    import { Component, Prop, Vue } from 'vue-property-decorator';

    @Component
    export default class TableListCell extends Vue {
        @Prop({default: false}) readonly isNumeric!: boolean;
        @Prop({default: () => {}}) readonly customClasses!: CssClasses;

        get cssClasses(): CssClasses{
            return {
                ...this.customClasses,
                'table-list__cell--numeric': this.isNumeric
            }
        }
    }
</script>
