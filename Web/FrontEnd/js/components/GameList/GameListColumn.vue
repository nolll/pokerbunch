<template>
    <th :class="cssClasses"><span class="table-list__column-header__content" v-on:click="sort">{{title}}</span></th>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { GAME_ARCHIVE } from '@/store-names';

    export default {
        props: ['name', 'title'],
        computed: {
            ...mapGetters(GAME_ARCHIVE, [
                'gameSortOrder'
            ]),
            isSelected() {
                return this.name === this.gameSortOrder;
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
                this.$store.dispatch('gameArchive/sortGames', this.name);
            }
        }
    };
</script>

<style>

</style>
