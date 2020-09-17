<template>
    <td :class="'table-list__cell table-list__cell--numeric' + winnerClass">
        <div v-if="isInGame">
            <span :class="'matrix__value' + resultClass">{{winnings}}</span>
            <span class="matrix__transaction">in {{buyin}}</span>
            <span class="matrix__transaction">out {{stack}}</span>
        </div>
    </td>
</template>

<script>
    export default {
        props: ['game'],
        computed: {
            buyin() {
                return this.game.buyin;
            },
            stack() {
                return this.game.stack;
            },
            winnings() {
                if (this.game.winnings > 0)
                    return '+' + this.game.winnings;
                return this.game.winnings;
            },
            winnerClass() {
                if (this.game.isWinner)
                    return ' matrix__winner';
                return '';
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
