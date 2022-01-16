import useBunches from './useBunches';
import format from '@/format';

export default function useFormatter() {
  const bunches = useBunches();

  const formatCurrency = (val: number): string => {
    return format.currency(val, bunches.currencyFormat.value, bunches.thousandSeparator.value);
  };

  const formatResult = (val: number): string => {
    return format.result(val, bunches.currencyFormat.value, bunches.thousandSeparator.value);
  };

  const formatResultWithoutCurrency = (val: number): string => {
    return format.resultWithoutCurrency(val);
  };

  const formatWinrate = (val: number): string => {
    return format.winrate(val, bunches.currencyFormat.value, bunches.thousandSeparator.value);
  };

  const formatDuration = (val: number): string => {
    return format.duration(val);
  };

  return {
    formatCurrency,
    formatResult,
    formatResultWithoutCurrency,
    formatWinrate,
    formatDuration,
  };
}
