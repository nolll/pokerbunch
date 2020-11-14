<template>
    <select :value="value" v-on:input="updateValue">
        <option value="">Select Event</option>
        <option
            v-for="(event) in events"
            :value="event.id"
            v-bind:key="event.id">
            {{event.name}}
        </option>
    </select>
</template>

<script lang="ts">
    import { Component, Mixins, Prop, Watch } from 'vue-property-decorator';
    import { EventMixin } from '@/mixins';
    
    @Component
    export default class EventDropdown extends Mixins(
        EventMixin
    ) {
        @Prop() value!: string;

        get events(){
            return this.$_events;
        }

        updateValue(event: any){
            this.$emit('input', event.target.value);
        }
    }
</script>
