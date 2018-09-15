<template>
    <th v-bind:class="'table-list__column-header table-list__column-header--sortable ' + sortColumnCssClass"><span class="table-list__column-header__content" v-on:click="sort">{{title}}</span></th>
</template>

<script>
    import { mapState } from 'vuex';

    export default {
        props: ['name', 'title'],
        computed: {
            ...mapState('gameList', ['orderBy']),
            isSelected: function () {
                return this.name === this.orderBy;
            },
            sortColumnCssClass: function () {
                return this.isSelected ? 'table-list--sortable__sort-column' : '';
            }
        },
        methods: {
            sort: function () {
                this.$store.dispatch('gameList/sortBy', this.name);
            }
        }
    };
</script>

<style>

</style>
