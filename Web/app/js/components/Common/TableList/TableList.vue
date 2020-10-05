<template>
    <table class="table-list">
        <slot></slot>
    </table>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component
    export default class TableList extends Vue {
    }
</script>
