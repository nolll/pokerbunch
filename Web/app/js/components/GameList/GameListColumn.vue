<template>
    <th :class="cssClasses"><span class="table-list__column-header__content" v-on:click="sort">{{title}}</span></th>
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
                return this.name === this.$_gameSortOrder;
            },
            cssClasses() {
                return {
                    'table-list__column-header': true,
                    'table-list__column-header--sortable': true,
                    'table-list--sortable__sort-column': this.isSelected
                }
            }
        },
        methods: {
            sort() {
                GameArchiveMixin.$_sortGames(this.name);
            }
        }
    };
</script>

<style>

</style>
