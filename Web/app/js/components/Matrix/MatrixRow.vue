<template>
    <tr class="table-list__row">
        <td class="table-list__cell table-list__cell--numeric">{{rank}}.</td>
        <td class="table-list__cell">
            <CustomLink :url="url">{{name}}</CustomLink>
        </td>
        <td :class="'table-list__cell table-list__cell--numeric' + resultClass">{{formattedWinnings}}</td>
        <td is="matrix-item" v-for="game in player.gameResults" :game="game" :key="game.gameId"></td>
    </tr>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import urls from '@/urls';
    import { FormatMixin } from '@/mixins'
    import MatrixItem from './MatrixItem.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';

    @Component({
        components: {
            MatrixItem,
            CustomLink
        }
    })
    export default class MatrixRow extends Mixins(
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

        get resultClass(): string {
            if (this.winnings > 0)
                return ' pos-result';
            if (this.winnings < 0)
                return ' neg-result';
            return '';
        }
    }
</script>
