<template>
    <tr class="table-list__row">
        <td class="table-list__cell table-list__cell--numeric">{{rank}}.</td>
        <td class="table-list__cell">
            <CustomLink :url="url">{{name}}</CustomLink>
        </td>
        <td class="table-list__cell table-list__cell--numeric" :class="resultClass">{{formattedWinnings}}</td>
        <td is="overview-item" :game="game"></td>
    </tr>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import { FormatMixin } from '@/mixins'
    import OverviewItem from './OverviewItem.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';
    import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
    import { CssClasses } from '@/models/CssClasses';

    @Component({
        components: {
            OverviewItem,
            CustomLink
        }
    })
    export default class OverviewRow extends Mixins(
        FormatMixin
    ) {
        @Prop() readonly player!: CashgameListPlayerData;
        @Prop() readonly index!: number;

        get url() {
            return urls.player.details(this.player.id);
        }

        get name() {
            return this.player.name;
        }

        get rank() {
            return this.index + 1;
        }

        get winnings() {
            return this.player.winnings;
        }

        get formattedWinnings() {
            return this.$_formatResult(this.winnings);
        }

        get resultClass(): CssClasses {
            return {
                'pos-result': this.winnings > 0,
                'neg-result': this.winnings < 0
            }
        }

        get game() {
            return this.player.gameResults[0];
        }
    }
</script>
