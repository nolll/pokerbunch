<template>
    <tr class="table-list__row">
        <slot></slot>
    </tr>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component
    export default class TableListRow extends Vue {
    }
</script>
