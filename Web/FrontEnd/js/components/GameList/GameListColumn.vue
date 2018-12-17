<template>
    <th :class="'table-list__column-header table-list__column-header--sortable ' + sortColumnCssClass"><span class="table-list__column-header__content" v-on:click="sort">{{title}}</span></th>
</template>

<script>
    import { mapState } from 'vuex';
    import { GAME_ARCHIVE } from '@/store-names';

    export default {
        props: ['name', 'title'],
        computed: {
            ...mapState(GAME_ARCHIVE, [
                'gameSortOrder'
            ]),
            isSelected() {
                return this.name === this.gameSortOrder;
            },
            sortColumnCssClass() {
                return this.isSelected ? 'table-list--sortable__sort-column' : '';
            }
        },
        methods: {
            sort() {
                this.$store.dispatch('gameArchive/sortGames', this.name);
            }
        }
    };
</script>

<style>

</style>
