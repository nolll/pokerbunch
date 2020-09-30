<template>
    <li class="simple-list__item">
        <slot></slot>
    </li>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component
    export default class SimpleListItem extends Vue {
    }
</script>
