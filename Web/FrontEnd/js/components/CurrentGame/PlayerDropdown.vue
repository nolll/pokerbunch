<template>
    <select v-model="selectedPlayerId" v-on:change="changePlayer">
        <option v-for="player in players" :value="player.id">{{player.name}}</option>
    </select>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { PLAYER, CASHGAME } from '@/store-names';

    export default {
        computed: {
            ...mapGetters(PLAYER, [
                'players'
            ]),
            ...mapGetters(CASHGAME, [
                'playerId'
            ])
        },
        methods: {
            changePlayer(event) {
                this.$store.dispatch('cashgame/selectPlayer', { playerId: this.selectedPlayerId });
            },
            setSelectedPlayerId(playerId) {
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
