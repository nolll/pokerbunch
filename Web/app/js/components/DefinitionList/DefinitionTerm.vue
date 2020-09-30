<template>
    <dt class="value-list__key">
        <slot></slot>
    </dt>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component
    export default class DefinitionTerm extends Vue {
    }
</script>
