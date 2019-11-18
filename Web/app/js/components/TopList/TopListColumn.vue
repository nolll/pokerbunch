<template>
    <th :class="'table-list__column-header table-list__column-header--sortable ' + sortColumnCssClass"><span class="table-list__column-header__content" v-on:click="sort">{{title}}</span></th>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { GAME_ARCHIVE } from '@/store-names';

    export default {
        props: ['name', 'title'],
        computed: {
            ...mapGetters(GAME_ARCHIVE, [
                'playerSortOrder'
            ]),
            isSelected() {
                return this.name === this.playerSortOrder;
            },
            sortColumnCssClass() {
                return this.isSelected ? 'table-list--sortable__sort-column' : '';
            }
        },
        methods: {
            sort() {
                this.$store.dispatch('gameArchive/sortPlayers', this.name);
            }
        }
    };
</script>

<style>

</style>
