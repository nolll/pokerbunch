<template>
    <div v-if="ready">
        <h2 class="h2">Single Game</h2>
        <definition-list>
            <definition-term>Best Result</definition-term>
            <player-result-fact :name="facts.bestResult.name" :amount="facts.bestResult.amount" />

            <definition-term>Worst Result</definition-term>
            <player-result-fact :name="facts.worstResult.name" :amount="facts.worstResult.amount" />
        </definition-list>
    </div>
</template>

<script>

    //Things to add
    //BiggestBuyin
    //BiggestCashout
    //BiggestComeback

    import { mapState, mapGetters } from 'vuex';
    import { FormatMixin } from '../../mixins'
    import { PlayerResultFact } from ".";
    import { DefinitionList, DefinitionTerm } from "../DefinitionList";

    export default {
        mixins: [
            FormatMixin
        ],
        components: {
            PlayerResultFact,
            DefinitionList,
            DefinitionTerm
        },
        computed: {
            ...mapGetters('gameArchive', ['sortedGames', 'sortedPlayers']),
            facts() {
                return getFacts(this.sortedPlayers);
            },
        },
        methods: {
            ready() {
                return this.bunchReady && this.sortedGames.length > 0;
            }
        }
    };

    function getFacts(players) {
        var bestResult = { name: '', id: 0, amount: 0 };
        var worstResult = { name: '', id: 0, amount: 0 };
        for (var pi = 0; pi < players.length; pi++) {
            var player = players[pi];
            for (var gi = 0; gi < player.games.length; gi++) {
                var game = player.games[gi];
                if (game && game.winnings > bestResult.amount) {
                    bestResult = { name: player.name, id: player.id, amount: game.winnings };
                }
                if (game && game.winnings < worstResult.amount) {
                    worstResult = { name: player.name, id: player.id, amount: game.winnings };
                }
            }
        }
        return {
            bestResult: bestResult,
            worstResult: worstResult
        }
    }
</script>

<style>

</style>
