<template>
    <select v-model="selectedPlayerId" v-on:change="changePlayer">
        <option v-for="player in players" v-bind:value="player.id">{{player.name}}</option>
    </select>
</template>

<script>
    export default {
        props: ['playerId', 'players'],
        methods: {
            changePlayer: function () {
                this.$store.dispatch('currentGame/selectPlayer', { playerId: this.selectedPlayerId });
            }
        },
        mounted: function () {
            this.selectedPlayerId = this.playerId;
        },
        data: function () {
            return {
                selectedPlayerId: null
            }
        }
    };
</script>

<style scoped>

</style>
