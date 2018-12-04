<template>
    <td class="table-list__cell table-list__cell--numeric">
        <div v-if="playedThisYear">
            <span :class="valueCss">{{winnings}}</span>
        </div>
    </td>
</template>

<script>
    export default {
        props: {
            year: {
                type: Object
            }
        },
        computed: {
            winnings() {
                if (this.year.winnings > 0)
                    return '+' + this.year.winnings;
                return this.year.winnings;
            },
            valueCss() {
                return {
                    'matrix__value': true,
                    'pos-result': this.winnings > 0,
                    'neg-result': this.winnings < 0
                };
            },
            playedThisYear() {
                return this.year.playedThisYear;
            }
        }
    };
</script>

<style>

</style>
