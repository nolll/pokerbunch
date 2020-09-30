<template>
    <dl class="value-list">
        <slot></slot>
    </dl>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component
    export default class ValueList extends Vue {
    }
</script>
