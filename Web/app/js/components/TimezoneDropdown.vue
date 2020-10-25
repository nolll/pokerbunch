<template>
    <select :value="value" v-on:input="updateValue">
        <option value="">Select timezone</option>
        <option
            v-for="(timezone) in timezones"
            :value="timezone.id"
            v-bind:key="timezone.id">
            {{timezone.name}}
        </option>
    </select>
</template>

<script lang="ts">
    import { Component, Mixins, Prop, Watch } from 'vue-property-decorator';
    import { TimezoneMixin } from '@/mixins';
    
    @Component
    export default class TimezoneDropdown extends Mixins(
        TimezoneMixin
    ) {
        @Prop() value!: string;

        get timezones(){
            return this.$_timezones;
        }

        updateValue(event: any){
            this.$emit('input', event.target.value)
        }
    }
</script>
