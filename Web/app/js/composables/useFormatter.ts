import useBunch from './useBunch';
import format from '@/format';

export default function useFormatter() {
    const bunch = useBunch();
  
    const formatCurrency = (val: number): string => {
        return format.currency(val, bunch.currencyFormat.value, bunch.thousandSeparator.value);
    };

    const formatResult = (val: number): string => {
        return format.result(val, bunch.currencyFormat.value, bunch.thousandSeparator.value);
    };

    const formatResultWithoutCurrency = (val: number): string => {
        return format.resultWithoutCurrency(val);
    };

    const formatWinrate = (val: number): string => {
        return format.winrate(val, bunch.currencyFormat.value, bunch.thousandSeparator.value);
    };

    const formatDuration = (val: number): string => {
        return format.duration(val);
    };

    return {
        formatCurrency,
        formatResult,
        formatResultWithoutCurrency,
        formatWinrate,
        formatDuration
    }
}
