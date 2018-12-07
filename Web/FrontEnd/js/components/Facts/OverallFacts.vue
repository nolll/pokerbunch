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
    import { mapGetters } from 'vuex';
    import { FormatMixin } from '../../mixins'
    import { AmountFact, TimeFact } from ".";
    import { DefinitionData, DefinitionList, DefinitionTerm } from "../DefinitionList";

    export default {
        mixins: [
            FormatMixin
        ],
        components: {
            AmountFact,
            TimeFact,
            DefinitionData,
            DefinitionList,
            DefinitionTerm
        },
        computed: {
            ...mapGetters('gameArchive', {
                sortedGames: getters => getters.sortedGames
            }),
            facts() {
                return getFacts(this.sortedGames);
            },
            gameCount() {
                return this.sortedGames.length;
            },
            turnover() {
                return this.formatCurrency(this.facts.turnover)
            }
        },
        methods: {
            ready() {
                return this.bunchReady && this.sortedGames.length > 0;
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
