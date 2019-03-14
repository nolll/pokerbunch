<template>
    <div>
        <div v-for="player in players">
            <player-row :player="player" :isCheckmarkEnabled="isRunning" :isReportTimeEnabled="isRunning"></player-row>
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
    import { mapGetters } from 'vuex';
    import { PlayerRow } from '.';
    import { FormatMixin } from '@/mixins'
    import { CASHGAME } from '@/store-names';

    export default {
        mixins: [
            FormatMixin
        ],
        components: {
            PlayerRow
        },
        props: ['players'],
        computed: {
            ...mapGetters(CASHGAME, [
                'totalBuyin',
                'totalStacks',
                'isRunning'
            ]),
            formattedTotalBuyin() {
                return this.formatCurrency(this.totalBuyin);
            },
            formattedTotalStacks() {
                return this.formatCurrency(this.totalStacks);
            }
        }
    };
</script>

<style>

</style>
