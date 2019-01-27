<template>
    <tr class="table-list__row">
        <td class="table-list__cell table-list__cell--numeric">{{rank}}.</td>
        <td class="table-list__cell">
            <custom-link :url="url">{{name}}</custom-link>
        </td>
        <td :class="'table-list__cell table-list__cell--numeric' + resultClass">{{formattedWinnings}}</td>
        <td is="year-matrix-item" v-for="year in player.years" :year="year"></td>
    </tr>
</template>

<script>
    import { FormatMixin } from '@/mixins'
    import { YearMatrixItem } from '.';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';

    export default {
        mixins: [
            FormatMixin
        ],
        props: ['player', 'index'],
        components: {
            YearMatrixItem,
            CustomLink
        },
        computed: {
            url() {
                return urls.player.details(this.player.id);
            },
            name() {
                return this.player.name;
            },
            rank() {
                return this.index + 1;
            },
            winnings() {
                return this.player.winnings;
            },
            formattedWinnings() {
                return this.formatResult(this.winnings);
            },
            resultClass() {
                if (this.winnings > 0)
                    return ' pos-result';
                if (this.winnings < 0)
                    return ' neg-result';
                return '';
            }
        }
    };
</script>

<style>

</style>
