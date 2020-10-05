<template>
    <button class="button button--icon">
        <i :class="iconCssClass"></i>
        {{text}}
    </button>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';

    @Component
    export default class GameButton extends Vue {
        @Prop() readonly text!: string;
        @Prop() readonly icon!: string;

        get iconCssClass(){
            return `icon-${this.icon}`;
        }
    }
</script>
