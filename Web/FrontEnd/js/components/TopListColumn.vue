<template>
    <th v-bind:class="'table-list__column-header table-list__column-header--sortable ' + sortColumnCssClass"><span class="table-list__column-header__content" v-on:click="sort">{{title}}</span></th>
</template>

<script>
    export default {
        props: ['name', 'title', 'orderBy'],
        computed: {
            sortingEnabled: function () {
                return true;
            },
            isSelected: function () {
                return this.name === this.orderBy;
            },
            sortColumnCssClass: function () {
                return this.isSelected ? 'table-list--sortable__sort-column' : '';
            }
        },
        methods: {
            sort: function () {
                this.eventHub.$emit('sort-by', this.name);
            }
        }
    };
</script>

<style scoped>

</style>
