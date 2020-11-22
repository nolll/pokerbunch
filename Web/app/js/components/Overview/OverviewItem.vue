<template>
    <TableListCell :is-numeric="true">
        <span class="matrix__value" v-if="isInGame"><WinningsText :value="winnings" /></span>
    </TableListCell>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { CashgamePlayerData } from '@/models/CashgamePlayerData';
    import { CssClasses } from '@/models/CssClasses';
    import TableListCell from '@/components/Common/TableList/TableListCell.vue';
    import WinningsText from '@/components/Common/WinningsText.vue';
    
    @Component({
        components: {
            TableListCell,
            WinningsText
        }
    })
    export default class OverviewItem extends Vue {
        @Prop() readonly game!: CashgamePlayerData;

        get buyin() {
            return this.game.buyin;
        }

        get stack() {
            return this.game.stack;
        }

        get winnings() {
            return this.game.winnings;
        }

        get isInGame() {
            return this.game.playedThisGame;
        }
    }
</script>
