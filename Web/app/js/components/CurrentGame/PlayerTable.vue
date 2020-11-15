<template>
    <div>
        <div v-for="player in players" v-bind:key="player.id">
            <PlayerRow :player="player" :isCashgameRunning="isCashgameRunning" :isReportTimeEnabled="isCashgameRunning" @selected="onSelected" @deleteAction="onDeleteAction" @saveAction="onSaveAction" :canEdit="canEdit" :bunchId="bunchId" />
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
    import { Component, Mixins, Prop } from 'vue-property-decorator';
    import PlayerRow from './PlayerRow.vue';
    import { FormatMixin } from '@/mixins'
    import { DetailedCashgameResponsePlayer } from '@/response/DetailedCashgameResponsePlayer';
    import cashgameHelper from '@/CashgameHelper';

    @Component({
        components: {
            PlayerRow
        }
    })
    export default class PlayerTable extends Mixins(
        FormatMixin
    ) {
        @Prop() readonly bunchId!: string;
        @Prop() readonly players!: DetailedCashgameResponsePlayer[];
        @Prop() readonly isCashgameRunning!: boolean;
        @Prop({default: false}) readonly canEdit!: boolean;

        get totalBuyin(){
            return cashgameHelper.getTotalBuyin(this.players);
        }

        get formattedTotalBuyin() {
            return this.$_formatCurrency(this.totalBuyin);
        }

        get totalStacks(){
            return cashgameHelper.getTotalStacks(this.players);
        }

        get formattedTotalStacks() {
            return this.$_formatCurrency(this.totalStacks);
        }

        onSelected(id: string){
            this.$emit('playerSelected', id);
        }

        onDeleteAction(id: string){
            this.$emit('deleteAction', id);
        }

        onSaveAction(data: any){
            this.$emit('saveAction', data);
        }
    }
</script>
