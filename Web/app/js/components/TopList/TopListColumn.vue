<template>
    <th class="table-list__column-header table-list__column-header--sortable" :class="sortColumnCssClasses"><span class="table-list__column-header__content" v-on:click="sort">{{title}}</span></th>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import { GameArchiveMixin } from '@/mixins';
    import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';

    @Component
    export default class TopListColumn extends Mixins(
        GameArchiveMixin
    ) {
        @Prop() readonly name!: CashgamePlayerSortOrder;
        @Prop() readonly title!: string;

        get isSelected() {
            return this.name === this.$_playerSortOrder;
        }

        get sortColumnCssClasses() {
            return {
                'table-list--sortable__sort-column': this.isSelected
            }
        }

        sort() {
            this.$_sortPlayers(this.name);
        }
    }
</script>
