<template>
    <div v-if="ready">
        <h2 class="h2">Single Game</h2>
        <DefinitionList>
            <DefinitionTerm>Best Result</DefinitionTerm>
            <PlayerResultFact :name="facts.bestResult.name" :amount="facts.bestResult.amount" />

            <DefinitionTerm>Worst Result</DefinitionTerm>
            <PlayerResultFact :name="facts.worstResult.name" :amount="facts.worstResult.amount" />
        </DefinitionList>
    </div>
</template>

<script lang="ts">

    //Things to add
    //BiggestBuyin
    //BiggestCashout
    //BiggestComeback

    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import PlayerResultFact from './PlayerResultFact.vue';
    import DefinitionList from '@/components/DefinitionList/DefinitionList.vue';
    import DefinitionTerm from '@/components/DefinitionList/DefinitionTerm.vue';
    import { BunchMixin, FormatMixin, GameArchiveMixin } from '@/mixins';
    import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
    import { SingleGameFactCollection } from '@/models/SingleGameFactCollection';
    import { PlayerWinningsFact } from '@/models/PlayerWinningsFact';

    @Component({
        components: {
            PlayerResultFact,
            DefinitionList,
            DefinitionTerm
        }
    })
    export default class SingleGameFacts extends Mixins(
        BunchMixin,
        FormatMixin,
        GameArchiveMixin
    ) {
        get facts() {
            return getFacts(this.$_sortedPlayers);
        }
        
        get ready() {
            return this.$_bunchReady && this.$_gamesReady;
        }
    }

    function getFacts(players: CashgameListPlayerData[]): SingleGameFactCollection {
        var bestResult: PlayerWinningsFact = { name: '', id: '0', amount: 0 };
        var worstResult: PlayerWinningsFact = { name: '', id: '0', amount: 0 };
        for (var pi = 0; pi < players.length; pi++) {
            var player = players[pi];
            for (var gi = 0; gi < player.gameResults.length; gi++) {
                var game = player.gameResults[gi];
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
