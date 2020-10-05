<template>
    <th class="table-list__column-header" :class="cssClasses">
        <span class="table-list__column-header__content" v-if="hasContent" v-on:click="onClick">
            <slot></slot>
        </span>
    </th>
</template>

<script lang="ts">
    import { CssClasses } from '@/models/CssClasses';
    import { Component, Prop, Vue } from 'vue-property-decorator';

    @Component
    export default class TableListColumnHeader extends Vue {
        @Prop() readonly sortName?: string | null;
        @Prop() readonly orderedBy?: string | null;

        get hasContent() {
            return !!this.$slots.default
        }

        get isSelected(){
            return this.sortName === this.orderedBy;
        }

        get isSortable(){
            return !!this.sortName;
        }

        get cssClasses(): CssClasses {
            return {
                'table-list__column-header--sortable': this.isSortable,
                'table-list__column-header--selected': this.isSortable && this.isSelected
            }
        }

        onClick(){
            if(this.isSortable)
                this.$emit('sort', this.sortName);
        }
    }
</script>
