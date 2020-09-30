<template>
    <dd class="value-list__value"><slot></slot></dd>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component
    export default class DefinitionData extends Vue {
    }
</script>
