<template>
    <select v-model.number="selectedPlayerId" v-on:change="changePlayer">
        <option v-for="player in bunchPlayers" v-bind:value="player.id">{{player.name}}</option>
    </select>
</template>

<script>
    import { mapState, mapGetters } from 'vuex';

    export default {
        computed: {
            ...mapState('currentGame', ['playerId', 'bunchPlayers'])
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
            'playerId': function (val) {
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

<style scoped>

</style>
