<template>
    <td class="table-list__cell table-list__cell--numeric">
        <div v-if="isInGame">
            <span :class="'matrix__value' + resultClass">{{formattedWinnings}}</span>
        </div>
    </td>
</template>

<script>
    import { mapState } from 'vuex';
    import moment from 'moment';
    import { FormatMixin } from '../../mixins';

    export default {
        props: ['game'],
        mixins: [
            FormatMixin
        ],
        computed: {
            buyin: function () {
                return this.game.buyin;
            },
            stack: function () {
                return this.game.stack;
            },
            winnings: function () {
                return this.game.winnings;
            },
            formattedWinnings: function () {
                return this.formatResult(this.winnings);
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
