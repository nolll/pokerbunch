<template>
    <li><a :href="url" :class="selectedClass" @click.prevent="click">{{text}}</a></li>
</template>

<script lang="ts">
import { CssClasses } from '@/models/CssClasses';
    import { Component, Prop, Vue } from 'vue-property-decorator';

    @Component
    export default class CashgameNavigationItem extends Vue {
        @Prop() readonly url!: string;
        @Prop() readonly text!: string;
        @Prop() readonly isSelected!: boolean;

        get selectedClass(): CssClasses {
            return {
                'selected': this.isSelected
            }
        }

        click() {
            this.$emit('selected', this.url);
        }
    }
</script>

<style lang="scss" scoped>
    li{
        margin-top: 5px;
    }
</style>
