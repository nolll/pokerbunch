import useBunch from './useBunch';
import format from '@/format';
import useParams from './useParams';

export default function useFormatter() {
  const params = useParams();
  const { bunch, bunchReady } = useBunch(params.slug.value);

  const formatCurrency = (val: number): string => {
    return bunchReady.value ? format.currency(val, bunch.value.currencyFormat, bunch.value.thousandSeparator) : val.toString();
  };

  const formatResult = (val: number): string => {
    return bunchReady.value ? format.result(val, bunch.value.currencyFormat, bunch.value.thousandSeparator) : val.toString();
  };

  const formatResultWithoutCurrency = (val: number): string => {
    return format.resultWithoutCurrency(val);
  };

  const formatWinrate = (val: number): string => {
    return bunchReady.value ? format.winrate(val, bunch.value.currencyFormat, bunch.value.thousandSeparator) : val.toString();
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
