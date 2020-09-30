<template>
    <tr class="table-list__row">
        <td class="table-list__cell table-list__cell--numeric table-list--sortable__base-column">{{player.rank}}.</td>
        <td class="table-list__cell table-list--sortable__base-column">
            <CustomLink :url="url">{{player.name}}</CustomLink>
        </td>
        <td class="table-list__cell table-list__cell--numeric" :class="winningsCssClass">{{formattedWinnings}}</td>
        <td class="table-list__cell table-list__cell--numeric">{{formattedBuyin}}</td>
        <td class="table-list__cell table-list__cell--numeric">{{formattedCashout}}</td>
        <td class="table-list__cell">{{formattedTime}}</td>
        <td class="table-list__cell table-list__cell--numeric">{{player.gameCount}}</td>
        <td class="table-list__cell table-list__cell--numeric">{{formattedWinrate}}</td>
    </tr>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import { FormatMixin } from '@/mixins';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';
    import { GameArchiveMixin } from '@/mixins';
    import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
    import { CssClasses } from '@/models/CssClasses';

    @Component({
        components: {
            CustomLink
        }
    })
    export default class TopListRow extends Mixins(
        FormatMixin,
        GameArchiveMixin
    ) {
        @Prop() readonly player!: CashgameListPlayerData;

        get url() {
            return urls.player.details(this.player.id);
        }
        
        get winningsCssClass(): CssClasses {
            const winnings = this.player.winnings;
            return {
                'pos-result': winnings > 0,
                'neg-result': winnings < 0
            }
        }

        get formattedWinnings() {
            return this.$_formatResult(this.player.winnings);
        }

        get formattedBuyin() {
            return this.$_formatCurrency(this.player.buyin);
        }

        get formattedCashout() {
            return this.$_formatCurrency(this.player.stack);
        }

        get formattedWinrate() {
            return this.$_formatWinrate(this.player.winrate);
        }

        get formattedTime() {
            return this.$_formatTime(this.player.playedTimeInMinutes);
        }
    }
</script>
