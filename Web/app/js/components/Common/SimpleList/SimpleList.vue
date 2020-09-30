<template>
    <ul class="simple-list">
        <slot></slot>
    </ul>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component
    export default class SimpleList extends Vue {
    }
</script>
