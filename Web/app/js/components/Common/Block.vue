<template>
    <div class="block">
        <slot></slot>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component
    export default class Block extends Vue {
    }
</script>
