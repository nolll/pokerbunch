<template>
    <select v-model="selectedPlayerId" v-on:change="changePlayer">
        <option v-for="player in bunchPlayers" :value="player.id">{{player.name}}</option>
    </select>
</template>

<script>
    import { mapState } from 'vuex';
    import { CURRENT_GAME } from '@/store-names';

    export default {
        computed: {
            ...mapState(CURRENT_GAME, {
                playerId: state => state.playerId,
                bunchPlayers: state => state.bunchPlayers
            })
        },
        methods: {
            changePlayer: function (event) {
                this.$store.dispatch('currentGame/selectPlayer', { playerId: this.selectedPlayerId });
            },
            setSelectedPlayerId: function (playerId) {
                this.selectedPlayerId = playerId;
            }
        },
        watch: {
            playerId: function (val) {
                this.setSelectedPlayerId(val);
            }
        },
        mounted: function () {
            this.setSelectedPlayerId(this.playerId);
        },
        data: function () {
            return {
                selectedPlayerId: null
            }
        }
    };
</script>

<style>

</style>
