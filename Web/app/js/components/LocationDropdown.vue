<template>
    <select :value="value" v-on:input="updateValue">
        <option value="">Select Location</option>
        <option
            v-for="(location) in locations"
            :value="location.id"
            v-bind:key="location.id">
            {{location.name}}
        </option>
    </select>
</template>

<script lang="ts">
    import { Component, Mixins, Prop, Watch } from 'vue-property-decorator';
    import { LocationMixin } from '@/mixins';
    
    @Component
    export default class LocationDropdown extends Mixins(
        LocationMixin
    ) {
        @Prop() value!: string;

        get locations(){
            return this.$_locations;
        }

        updateValue(event: any){
            this.$emit('input', event.target.value);
        }
    }
</script>
