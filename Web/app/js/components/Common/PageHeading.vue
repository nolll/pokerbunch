<template>
    <h1 class="page-heading">{{text}}</h1>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';

    @Component
    export default class PageHeading extends Vue {
        @Prop() readonly text!: string;
    }
</script>
