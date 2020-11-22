<template>
    <div v-if="ready">
        <h2 class="h2">Overall</h2>
        <DefinitionList>
            <DefinitionTerm>Number of games</DefinitionTerm>
            <DefinitionData>{{gameCount}}</DefinitionData>

            <DefinitionTerm>Total Time Played</DefinitionTerm>
            <DefinitionData><DurationText :value="facts.duration" /></DefinitionData>

            <DefinitionTerm>Total Turnover</DefinitionTerm>
            <DefinitionData><CurrencyText :value="turnover" /></DefinitionData>
        </DefinitionList>
    </div>
</template>

<script lang="ts">
    import { Component, Mixins } from 'vue-property-decorator';
    import DefinitionList from '@/components/DefinitionList/DefinitionList.vue';
    import DefinitionData from '@/components/DefinitionList/DefinitionData.vue';
    import DefinitionTerm from '@/components/DefinitionList/DefinitionTerm.vue';
    import DurationText from '@/components/Common/DurationText.vue';
    import CurrencyText from '@/components/Common/CurrencyText.vue';
    import { BunchMixin, FormatMixin, GameArchiveMixin } from '@/mixins';
    import { ArchiveCashgame } from '@/models/ArchiveCashgame';
    import { OverallFactCollection } from '@/models/OverallFactCollection';
    
    @Component({
        components: {
            CurrencyText,
            DefinitionData,
            DefinitionList,
            DefinitionTerm,
            DurationText
        }
    })
    export default class OverallFacts extends Mixins(
        BunchMixin,
        FormatMixin,
        GameArchiveMixin
    ){
        get facts() {
            return getFacts(this.$_sortedGames);
        }

        get gameCount() {
            return this.$_sortedGames.length;
        }

        get turnover() {
            return this.$_formatCurrency(this.facts.turnover)
        }

        get ready() {
            return this.$_bunchReady && this.$_gamesReady;
        }
    }

    function getFacts(games: ArchiveCashgame[]): OverallFactCollection {
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
