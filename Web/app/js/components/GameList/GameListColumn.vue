<template>
    <th :class="cssClasses"><span class="table-list__column-header__content" v-on:click="sort">{{title}}</span></th>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import { GameArchiveMixin } from '@/mixins';
    import { CssClasses } from '@/models/CssClasses';

    @Component
    export default class GameListColumn extends Mixins(
        GameArchiveMixin
    ) {
        @Prop(String) readonly name!: string;
        @Prop(String) readonly title!: string;

        get isSelected() {
            return this.name === this.$_gameSortOrder;
        }

        get cssClasses(): CssClasses {
            return {
                'table-list__column-header': true,
                'table-list__column-header--sortable': true,
                'table-list--sortable__sort-column': this.isSelected
            }
        }

        sort() {
            this.$_sortGames(this.name);
        }
    }
</script>
