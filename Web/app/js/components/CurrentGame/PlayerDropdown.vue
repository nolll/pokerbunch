<template>
    <select v-model="selectedPlayerId" v-on:change="changePlayer">
        <option v-for="player in players" :value="player.id" v-bind:key="player.id">{{player.name}}</option>
    </select>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { CashgameMixin, PlayerMixin } from '@/mixins';
    
    @Component
    export default class PlayerDropdown extends Mixins(
        CashgameMixin,
        PlayerMixin
    ) {
        selectedPlayerId: string | null = null;

        get players(){
            return this.$_players;
        }

        changePlayer() {
            if(this.selectedPlayerId)
                this.$_selectPlayer(this.selectedPlayerId);
        }

        setSelectedPlayerId(playerId: string) {
            this.selectedPlayerId = playerId;
        }

        mounted(){
            this.setSelectedPlayerId(this.$_playerId);
        }

        @Watch('$_playerId')
        playerIdChanged(val: string) {
            this.setSelectedPlayerId(val);
        }
    }
</script>
