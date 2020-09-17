<template>
    <div v-if="ready">
        <h2 class="h2">Overall</h2>
        <definition-list>
            <definition-term>Number of games</definition-term>
            <definition-data>{{gameCount}}</definition-data>

            <definition-term>Total Time Played</definition-term>
            <time-fact :minutes="facts.duration" />

            <definition-term>Total Turnover</definition-term>
            <amount-fact :amount="turnover" />
        </definition-list>
    </div>
</template>

<script>
    import { AmountFact, TimeFact } from '.';
    import { DefinitionData, DefinitionList, DefinitionTerm } from '@/components/DefinitionList';
    import { BunchMixin, FormatMixin, GameArchiveMixin } from '@/mixins';

    export default {
        components: {
            AmountFact,
            TimeFact,
            DefinitionData,
            DefinitionList,
            DefinitionTerm
        },
        mixins: [
            BunchMixin,
            FormatMixin,
            GameArchiveMixin
        ],
        computed: {
            facts() {
                return getFacts(this.$_sortedGames);
            },
            gameCount() {
                return this.$_sortedGames.length;
            },
            turnover() {
                return this.$_formatCurrency(this.facts.turnover)
            }
        },
        methods: {
            ready() {
                return this.$_bunchReady && this.$_gamesReady;
            }
        }
    };

    function getFacts(games) {
        var duration = 0;
        var turnover = 0;
        for (var gi = 0; gi < games.length; gi++) {
            var game = games[gi];
            duration += game.duration;
            turnover += game.turnover;
        }
        return {
            duration: duration,
            turnover: turnover
        }
    }
</script>

<style>

</style>
