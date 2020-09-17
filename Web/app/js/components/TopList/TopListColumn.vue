<template>
    <th :class="'table-list__column-header table-list__column-header--sortable ' + sortColumnCssClass"><span class="table-list__column-header__content" v-on:click="sort">{{title}}</span></th>
</template>

<script>
    import { GameArchiveMixin } from '@/mixins';

    export default {
        props: ['name', 'title'],
        mixins: [
            GameArchiveMixin
        ],
        computed: {
            isSelected() {
                return this.name === this.$_playerSortOrder;
            },
            sortColumnCssClass() {
                return this.isSelected ? 'table-list--sortable__sort-column' : '';
            }
        },
        methods: {
            sort() {
                this.$_sortPlayers(this.name);
            }
        }
    };
</script>

<style>

</style>
