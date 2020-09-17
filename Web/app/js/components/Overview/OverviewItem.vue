<template>
    <td class="table-list__cell table-list__cell--numeric">
        <div v-if="isInGame">
            <span :class="'matrix__value' + resultClass">{{formattedWinnings}}</span>
        </div>
    </td>
</template>

<script>
    import { FormatMixin } from '@/mixins';

    export default {
        props: ['game'],
        mixins: [
            FormatMixin
        ],
        computed: {
            buyin() {
                return this.game.buyin;
            },
            stack() {
                return this.game.stack;
            },
            winnings() {
                return this.game.winnings;
            },
            formattedWinnings() {
                return this.$_formatResult(this.winnings);
            },
            resultClass() {
                if (this.winnings > 0)
                    return ' pos-result';
                if (this.winnings < 0)
                    return ' neg-result';
                return '';
            },
            isInGame() {
                return !!this.game;
            }
        }
    };
</script>

<style>

</style>
