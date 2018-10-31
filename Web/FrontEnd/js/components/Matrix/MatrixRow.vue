<template>
    <tr class="table-list__row">
        <td class="table-list__cell table-list__cell--numeric">{{rank}}.</td>
        <td class="table-list__cell">
            <a :href="url">{{name}}</a>
        </td>
        <td :class="'table-list__cell table-list__cell--numeric' + resultClass">{{formattedWinnings}}</td>
        <td is="matrix-item" v-for="game in player.games" :game="game"></td>
    </tr>
</template>

<script>
    import { mapState } from 'vuex';
    import moment from 'moment';
    import { FormatMixin } from '../../mixins'
    import { MatrixItem } from ".";

    export default {
        mixins: [
            FormatMixin
        ],
        props: ['player', 'index'],
        components: {
            MatrixItem
        },
        computed: {
            url: function () {
                return '/player/details/' + this.player.id;
            },
            name: function () {
                return this.player.name;
            },
            rank: function () {
                return this.index + 1;
            },
            winnings: function () {
                return this.player.winnings;
            },
            formattedWinnings: function (result) {
                return this.formatResult(this.winnings);
            },
            resultClass: function () {
                if (this.winnings > 0)
                    return ' pos-result';
                if (this.winnings < 0)
                    return ' neg-result';
                return '';
            },
            game: function () {
                return this.player.game;
            }
        }
    };
</script>

<style>

</style>
