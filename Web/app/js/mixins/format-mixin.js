import format from '@/format';
import { BunchMixin } from '@/mixins';

export default {
    mixins: [
        BunchMixin
    ],
    methods: {
        $_formatCurrency(val) {
            return format.currency(val, this.$_currencyFormat, this.$_thousandSeparator);
        },
        $_formatResult(val) {
            return format.result(val, this.$_currencyFormat, this.$_thousandSeparator);
        },
        $_formatWinrate(val) {
            return format.winrate(val, this.$_currencyFormat, this.$_thousandSeparator);
        },
        $_formatTime(val) {
            return format.time(val);
        }
    }
};
