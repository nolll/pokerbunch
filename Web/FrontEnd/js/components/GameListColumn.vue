<template>
    <th v-bind:class="'table-list__column-header table-list__column-header--sortable ' + defaultColumnCssClass + ' ' + sortColumnCssClass"><span class="table-list__column-header__content" v-on:click="sort">{{title}}</span></th>
</template>

<script>
    export default {
        props: ['name', 'title', 'orderBy', 'isDefault'],
        computed: {
            sortingEnabled: function () {
                return true;
            },
            isSelected: function () {
                return this.name === this.orderBy;
            },
            sortColumnCssClass: function () {
                return this.isSelected ? 'table-list--sortable__sort-column' : '';
            },
            defaultColumnCssClass: function () {
                return this.isDefault ? 'table-list--sortable__base-column' : '';
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
