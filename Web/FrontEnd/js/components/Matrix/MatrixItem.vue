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
    import { mapState } from 'vuex';
    import moment from 'moment';

    export default {
        props: ['game'],
        computed: {
            buyin: function () {
                return this.game.buyin;
            },
            stack: function () {
                return this.game.stack;
            },
            winnings: function () {
                if (this.game.winnings > 0)
                    return '+' + this.game.winnings;
                return this.game.winnings;
            },
            winnerClass: function () {
                if (this.game.isWinner)
                    return ' matrix__winner';
                return '';
            },
            resultClass: function () {
                if (this.winnings > 0)
                    return ' pos-result';
                if (this.winnings < 0)
                    return ' neg-result';
                return '';
            },
            isInGame: function () {
                return !!this.game;
            }
        }
    };
</script>

<style>

</style>
