<template>
    <div v-if="ready">
        <h2 class="h2">Totals</h2>
        <DefinitionList>
            <DefinitionTerm>Most Time Played</DefinitionTerm>
            <PlayerTime-fact :name="facts.mostTime.name" :minutes="facts.mostTime.minutes" />

            <DefinitionTerm>Best Total Result</DefinitionTerm>
            <PlayerResultFact :name="facts.bestTotal.name" :amount="facts.bestTotal.amount" />

            <DefinitionTerm>Worst Total Result</DefinitionTerm>
            <PlayerResultFact :name="facts.worstTotal.name" :amount="facts.worstTotal.amount" />

            <DefinitionTerm>Biggest Total Buyin</DefinitionTerm>
            <PlayerResultFact :name="facts.biggestBuyin.name" :amount="facts.biggestBuyin.amount" />

            <DefinitionTerm>Biggest Total Cashout</DefinitionTerm>
            <PlayerResultFact :name="facts.biggestCashout.name" :amount="facts.biggestCashout.amount" />
        </DefinitionList>
    </div>
</template>

<script lang="ts">

    //Things to add
    //MostGamesPlayed
    //HighestWinrate

    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import PlayerAmountFact from './PlayerAmountFact.vue';
    import PlayerResultFact from './PlayerResultFact.vue';
    import PlayerTimeFact from './PlayerTimeFact.vue';
    import DefinitionList from '@/components/DefinitionList/DefinitionList.vue';
    import DefinitionTerm from '@/components/DefinitionList/DefinitionTerm.vue';
    import { BunchMixin, FormatMixin, GameArchiveMixin } from '@/mixins';
    import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
    import { TotalFactCollection } from '@/models/TotalFactCollection';
    
    @Component({
        components: {
            PlayerAmountFact,
            PlayerResultFact,
            PlayerTimeFact,
            DefinitionList,
            DefinitionTerm
        }
    })
    export default class TotalFacts extends Mixins(
        BunchMixin,
        FormatMixin,
        GameArchiveMixin
    ) {
        @Prop(String) readonly text!: string;

        get facts() {
            return getFacts(this.$_sortedPlayers);
        }

        get ready() {
            return this.$_bunchReady && this.$_sortedGames.length > 0;
        }
    }

    function getFacts(players: CashgameListPlayerData[]): TotalFactCollection {
        var mostTime = { name: '', id: '0', minutes: 0 };
        var bestTotal = { name: '', id: '0', amount: 0 };
        var worstTotal = { name: '', id: '0', amount: 0 };
        var biggestBuyin = { name: '', id: '0', amount: 0 };
        var biggestCashout = { name: '', id: '0', amount: 0 };
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
