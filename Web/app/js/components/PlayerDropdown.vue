<template>
    <select :value="value" v-on:input="updateValue">
        <option v-for="player in players" :value="player.id" v-bind:key="player.id">{{player.name}}</option>
    </select>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { Player } from '@/models/Player';
    
    @Component
    export default class PlayerDropdown extends Vue {
        @Prop() value!: string;
        @Prop() readonly players!: Player[];

        updateValue(event: any){
            this.$emit('input', event.target.value);
        }
    }
</script>
