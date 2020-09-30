<template>
    <div>
        <div v-for="player in players" v-bind:key="player.id">
            <PlayerRow :player="player" :isCheckmarkEnabled="$_isRunning" :isReportTimeEnabled="$_isRunning" />
        </div>
        <div class="totals">
            <div class="title">Totals: </div>
            <div class="amounts">
                <div class="amount"><i title="Total Buy in" class="icon-signin"></i> <span>{{formattedTotalBuyin}}</span></div>
                <div class="amount"><i title="Total Stacks" class="icon-reorder"></i> <span>{{formattedTotalStacks}}</span></div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Mixins } from 'vue-property-decorator';
    import PlayerRow from './PlayerRow.vue';
    import { CashgameMixin, FormatMixin } from '@/mixins'

    @Component({
        components: {
            PlayerRow
        }
    })
    export default class PlayerTable extends Mixins(
        CashgameMixin,
        FormatMixin
    ) {
        get players(){
            return this.$_sortedPlayers;
        }

        get formattedTotalBuyin() {
            return this.$_formatCurrency(this.$_totalBuyin);
        }

        get formattedTotalStacks() {
            return this.$_formatCurrency(this.$_totalStacks);
        }
    }
</script>
