import { Component, Mixins } from 'vue-property-decorator';
import format from '@/format';
import { BunchMixin } from '@/mixins';

@Component
export class FormatMixin extends Mixins(BunchMixin) {
    protected $_formatCurrency(val: number): string {
        return format.currency(val, this.$_currencyFormat, this.$_thousandSeparator);
    }

    protected $_formatResult(val: number): string {
        return format.result(val, this.$_currencyFormat, this.$_thousandSeparator);
    }

    protected $_formatWinrate(val: number): string {
        return format.winrate(val, this.$_currencyFormat, this.$_thousandSeparator);
    }

    protected $_formatDuration(val: number): string {
        return format.duration(val);
    }
}
