import { mapState } from 'vuex';
import format from '../format';

export default {
    computed: {
        ...mapState('bunch', ['currencyFormat', 'thousandSeparator'])
    },
    methods: {
        formatCurrency(val) {
            return format.currency(val, this.currencyFormat, this.thousandSeparator);
        },
        formatResult(val) {
            return format.result(val, this.currencyFormat, this.thousandSeparator);
        },
        formatWinrate(val) {
            return format.winrate(val, this.currencyFormat, this.thousandSeparator);
        },
        formatTime(val) {
            return format.time(val);
        }
    }
};
