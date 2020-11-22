<template>
    <TableListRow>
        <TableListCell :is-numeric="true">{{player.rank}}.</TableListCell>
        <TableListCell><CustomLink :url="url">{{player.name}}</CustomLink></TableListCell>
        <TableListCell :is-numeric="true"><WinningsText :value="winnings" /></TableListCell>
        <TableListCell :is-numeric="true"><CurrencyText :value="buyin" /></TableListCell>
        <TableListCell :is-numeric="true"><CurrencyText :value="cashout" /></TableListCell>
        <TableListCell><DurationText :value="time" /></TableListCell>
        <TableListCell :is-numeric="true">{{player.gameCount}}</TableListCell>
        <TableListCell :is-numeric="true"><WinrateText :value="winrate" /></TableListCell>
    </TableListRow>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';
    import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
    import { CssClasses } from '@/models/CssClasses';
    import TableListRow from '@/components/Common/TableList/TableListRow.vue';
    import TableListCell from '@/components/Common/TableList/TableListCell.vue';
    import WinningsText from '@/components/Common/WinningsText.vue';
    import WinrateText from '@/components/Common/WinrateText.vue';
    import CurrencyText from '@/components/Common/CurrencyText.vue';
    import DurationText from '@/components/Common/DurationText.vue';

    @Component({
        components: {
            CurrencyText,
            CustomLink,
            DurationText,
            TableListRow,
            TableListCell,
            WinningsText,
            WinrateText
        }
    })
    export default class TopListRow extends Vue {
        @Prop() readonly bunchId!: string;
        @Prop() readonly player!: CashgameListPlayerData;

        get url() {
            return urls.player.details(this.bunchId, this.player.id);
        }
        
        get winnings() {
            return this.player.winnings;
        }

        get buyin() {
            return this.player.buyin;
        }

        get cashout() {
            return this.player.stack;
        }

        get winrate() {
            return this.player.winrate;
        }

        get time() {
            return this.player.playedTimeInMinutes;
        }
    }
</script>
