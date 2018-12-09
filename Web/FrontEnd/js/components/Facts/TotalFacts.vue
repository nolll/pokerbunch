<template>
    <div v-if="ready">
        <h2 class="h2">Totals</h2>
        <definition-list>
            <definition-term>Most Time Played</definition-term>
            <player-time-fact :name="facts.mostTime.name" :minutes="facts.mostTime.minutes" />

            <definition-term>Best Total Result</definition-term>
            <player-result-fact :name="facts.bestTotal.name" :amount="facts.bestTotal.amount" />

            <definition-term>Worst Total Result</definition-term>
            <player-result-fact :name="facts.worstTotal.name" :amount="facts.worstTotal.amount" />

            <definition-term>Biggest Total Buyin</definition-term>
            <player-amount-fact :name="facts.biggestBuyin.name" :amount="facts.biggestBuyin.amount" />

            <definition-term>Biggest Total Cashout</definition-term>
            <player-amount-fact :name="facts.biggestCashout.name" :amount="facts.biggestCashout.amount" />
        </definition-list>
    </div>
</template>

<script>

    //Things to add
    //MostGamesPlayed
    //HighestWinrate

    import { mapGetters } from 'vuex';
    import { FormatMixin } from '@/mixins';
    import { PlayerAmountFact, PlayerResultFact, PlayerTimeFact } from ".";
    import { DefinitionList, DefinitionTerm } from "../DefinitionList";
    import { GAME_ARCHIVE } from '@/store-names';

    export default {
        mixins: [
            FormatMixin
        ],
        components: {
            PlayerAmountFact,
            PlayerResultFact,
            PlayerTimeFact,
            DefinitionList,
            DefinitionTerm
        },
        computed: {
            ...mapGetters(GAME_ARCHIVE, {
                sortedGames: getters => getters.sortedGames,
                sortedPlayers: getters => getters.sortedPlayers
            }),
            facts() {
                return getFacts(this.sortedPlayers);
            }
        },
        methods: {
            ready() {
                return this.bunchReady && this.sortedGames.length > 0;
            }
        }
    };

    function getFacts(players) {
        var mostTime = { name: '', id: 0, minutes: 0 };
        var bestTotal = { name: '', id: 0, amount: 0 };
        var worstTotal = { name: '', id: 0, amount: 0 };
        var biggestBuyin = { name: '', id: 0, amount: 0 };
        var biggestCashout = { name: '', id: 0, amount: 0 };
        for (var pi = 0; pi < players.length; pi++) {
            var player = players[pi];
            if (player.playedTimeInMinutes > mostTime.minutes) {
                mostTime = { name: player.name, id: player.id, minutes: player.playedTimeInMinutes };
            }
            if (player.winnings > bestTotal.amount) {
                bestTotal = { name: player.name, id: player.id, amount: player.winnings };
            }
            if (player.winnings < worstTotal.amount) {
                worstTotal = { name: player.name, id: player.id, amount: player.winnings };
            }
            if (player.buyin > biggestBuyin.amount) {
                biggestBuyin = { name: player.name, id: player.id, amount: player.buyin };
            }
            if (player.stack > biggestCashout.amount) {
                biggestCashout = { name: player.name, id: player.id, amount: player.stack };
            }
        }
        return {
            mostTime: mostTime,
            bestTotal: bestTotal,
            worstTotal: worstTotal,
            biggestBuyin: biggestBuyin,
            biggestCashout: biggestCashout
        }
    }
</script>

<style>
</style>
