<template>
    <select :value="value" v-on:input="updateValue">
        <option value="">All years</option>
        <option
            v-for="year in years"
            :value="year"
            v-bind:key="year">
            {{year}}
        </option>
    </select>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { LocationMixin } from '@/mixins';
    
    @Component
    export default class YearDropdown extends Vue {
        @Prop() value!: string;
        @Prop() years!: number[];

        updateValue(event: any){
            this.$emit('input', event.target.value);
        }
    }
</script>
