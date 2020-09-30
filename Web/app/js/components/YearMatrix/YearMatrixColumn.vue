<template>
    <th class="table-list__column-header">
        <span class="table-list__column-header__content">{{year}}</span>
    </th>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';

    @Component
    export default class YearMatrixColumn extends Vue {
        @Prop() readonly year!: number;
    }
</script>
