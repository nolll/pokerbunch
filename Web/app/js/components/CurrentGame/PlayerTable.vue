<template>
    <div>
        <div v-for="player in $_sortedPlayers">
            <player-row :player="player" :isCheckmarkEnabled="$_isRunning" :isReportTimeEnabled="$_isRunning" />
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

<script>
    import { PlayerRow } from '.';
    import { CashgameMixin, FormatMixin } from '@/mixins'

    export default {
        mixins: [
            CashgameMixin,
            FormatMixin
        ],
        components: {
            PlayerRow
        },
        computed: {
            formattedTotalBuyin() {
                return this.$_formatCurrency(this.$_totalBuyin);
            },
            formattedTotalStacks() {
                return this.$_formatCurrency(this.$_totalStacks);
            }
        }
    };
</script>

<style>

</style>
