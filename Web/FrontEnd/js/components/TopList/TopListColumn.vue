<template>
    <th :class="'table-list__column-header table-list__column-header--sortable ' + sortColumnCssClass"><span class="table-list__column-header__content" v-on:click="sort">{{title}}</span></th>
</template>

<script>
    import { mapState } from 'vuex';

    export default {
        props: ['name', 'title'],
        computed: {
            ...mapState('gameArchive', ['playerSortOrder']),
            isSelected: function () {
                return this.name === this.playerSortOrder;
            },
            sortColumnCssClass: function () {
                return this.isSelected ? 'table-list--sortable__sort-column' : '';
            }
        },
        methods: {
            sort: function () {
                this.$store.dispatch('gameArchive/sortPlayers', this.name);
            }
        }
    };
</script>

<style>

</style>
