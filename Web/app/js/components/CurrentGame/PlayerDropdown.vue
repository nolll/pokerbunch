<template>
    <select v-model="selectedPlayerId" v-on:change="changePlayer">
        <option v-for="player in $_players" :value="player.id">{{player.name}}</option>
    </select>
</template>

<script>
    import { CashgameMixin, PlayerMixin } from '@/mixins';

    export default {
        mixins: [
            CashgameMixin,
            PlayerMixin
        ],
        methods: {
            changePlayer(event) {
                this.$_selectPlayer(this.selectedPlayerId);
            },
            setSelectedPlayerId(playerId) {
                this.selectedPlayerId = playerId;
            }
        },
        watch: {
            $_playerId: function (val) {
                this.setSelectedPlayerId(val);
            }
        },
        mounted: function () {
            this.setSelectedPlayerId(this.$_playerId);
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
